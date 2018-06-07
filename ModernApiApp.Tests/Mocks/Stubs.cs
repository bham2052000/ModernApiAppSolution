using Microsoft.EntityFrameworkCore;
using ModernApiApp.DataAccess;
using ModernApiApp.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModernApiApp.Tests.Mocks
{
    public class Stubs
    {
        private static ApiContext _apiContext;

        public static IUnitOfWork<ApiContext> setUpUnitOfWorkMock()
        {
            var options = new DbContextOptionsBuilder<ApiContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryApiAppDatabase")
                .Options;

            _apiContext = new ApiContext(options);
            for (int i = 0; i < 100; i++)
            {
                _apiContext.Customers.Add(new Customer { Id = i + 1, Name = $"Name {i}", Address = $"{(i + 1) * 10} main street" });
            }
            _apiContext.SaveChanges();
            var uow = new UnitOfWork<ApiContext>(_apiContext);
            return uow;
        }
    }
}
