using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ModernApiApp.DataAccess;
using ModernApiApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace ModernApiApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = buildConnectionString();
            services.AddDbContext<ApiContext>(options => options.UseSqlServer(
                                    connectionString,
                                    b => b.MigrationsAssembly("ModernApiApp")
                                    )
                                  );
                                  //User Id=sa;Password=Microsoft!1
            services.AddTransient<IUnitOfWork<ApiContext>, UnitOfWork<ApiContext>>();
            services.AddCors();
            services.AddMvc();
        }

        private string buildConnectionString()
        {
            string apiAppDbServerName = Environment.GetEnvironmentVariable("ApiAppDatabaseServer");
            if (string.Equals(apiAppDbServerName, "localdb"))
            {
                apiAppDbServerName = "(localdb)\\MSSQLLocalDB";
            }
            string apiAppDbName = Environment.GetEnvironmentVariable("ApiAppDatabaseName");
            string apiAppDatabaseUser = Environment.GetEnvironmentVariable("ApiAppDatabaseUser");
            string apiAppDatabasePassword = Environment.GetEnvironmentVariable("ApiAppDatabasePassword");
            string apiAppDbConnectionString;
            if(!String.IsNullOrEmpty(apiAppDatabaseUser))
            {
                apiAppDbConnectionString = $"Server={apiAppDbServerName};Database={apiAppDbName};UID={apiAppDatabaseUser};PASSWORD={apiAppDatabasePassword}; ";
            }
            else
            {
                apiAppDbConnectionString = $"Server={apiAppDbServerName};Database={apiAppDbName};Trusted_Connection=True";
            }
            return apiAppDbConnectionString;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
            //}

            app.UseMvc();
        }
    }
}
