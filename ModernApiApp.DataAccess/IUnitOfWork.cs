using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModernApiApp.DataAccess
{
    public interface IUnitOfWork<TDbContext> where TDbContext : DbContext, new()
    {
        TDbContext DbContext { get; }
        System.Threading.Tasks.Task<int> SaveAsync();
    }
}
