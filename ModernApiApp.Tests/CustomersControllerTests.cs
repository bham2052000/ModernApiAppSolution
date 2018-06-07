using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernApiApp.Controllers;
using ModernApiApp.Entities;
using Microsoft.AspNetCore.Mvc;
using ModernApiApp.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ModernApiApp.Tests.Mocks;

namespace ModernApiApp.Tests
{
    [TestClass]
    public class CustomersControllerTests
    {
        IUnitOfWork<ApiContext> _uow;
        Repository<Customer> _customerRepository;
        
       
        public CustomersControllerTests()
        {
            _uow = Stubs.setUpUnitOfWorkMock();
            _customerRepository = new Repository<Customer>(_uow.DbContext);
        }

        [TestMethod]
        public void TestGetCustomerById_ShouldReturnAValidCustomerWhenGivenAValidId()
        {
            //Arrange
            var customersControllerUnderTest = new CustomersController(_uow);
            int idUnderTest = _customerRepository.GetAll().FirstOrDefault().Id;
            //Act
            var returnedResult = customersControllerUnderTest.GetById(idUnderTest);
            //Assert
            Assert.IsNotNull(returnedResult, $"The GetbyId method didn't return any customer for ID= {idUnderTest}");
            Assert.AreEqual(returnedResult.GetType(), typeof(OkObjectResult),
             $"The GetbyId returned type: {returnedResult.GetType()} instead of {typeof(OkObjectResult)}");
            Assert.IsInstanceOfType((returnedResult as OkObjectResult).Value, typeof(Customer),
             $"The object result returned GetbyId contains type: {(returnedResult as OkObjectResult).Value.GetType()} instead of {typeof(Customer)}");
            Customer returnedCustomer = (Customer)(returnedResult as OkObjectResult).Value;
            Assert.AreEqual(idUnderTest, returnedCustomer.Id,
             $"The customer result returned GetbyId has Id: {returnedCustomer.Id} instead of {idUnderTest}");
        }
    }
}
