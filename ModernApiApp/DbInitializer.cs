using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ModernApiApp.DataAccess;
using System.Linq;
using ModernApiApp.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace ModernApiApp
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            
            using (var context = new ApiContext(
                 serviceProvider.GetRequiredService<DbContextOptions<ApiContext>>()))
            {
                context.Database.EnsureCreated();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Customers ON");

                // Look for any customers.
                if (context.Customers.Any())
                {
                    return;   // DB has been seeded
                }
                var customers = new List<Customer>();
                for (int i = 0; i < 100; i++)
                {
                    var customer = new Customer { Name = $"Customer {i + 1}", Address = $"{(i + 1) * 100} Main St" };
                    customers.Add(customer);
                }
                foreach (Customer s in customers)
                {
                    context.Customers.Add(s);
                }

                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Customers OFF");
            }
            
           
           


        }
    }
}
