using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ModernApiApp.Entities;
using System;
using System.IO;

namespace ModernApiApp.DataAccess
{
    public partial class ApiContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public ApiContext()
        {

        }
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

    }
}
