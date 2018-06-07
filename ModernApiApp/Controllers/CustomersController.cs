using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModernApiApp.Entities;
using ModernApiApp.DataAccess;

namespace ModernApiApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Customers")]
    public class CustomersController : Controller
    {
        IUnitOfWork<ApiContext> _uow;
        IRepository<Customer> _customerRepository;
        public CustomersController(IUnitOfWork<ApiContext> uow)
        {
            _uow = uow;
            _customerRepository = new Repository<Customer>(_uow.DbContext);
        }
        // GET api/customers
        [HttpGet]
        public IActionResult Get()
        {
            var customers = _customerRepository.GetAll().Take(10); 
            return Ok(customers.ToArray());
        }

        public IActionResult GetById(int id)
        {
            Customer customer = null;
             if (id > 0)
            {
                customer = _customerRepository.GetByIdAsync(id).Result;
                return Ok(customer);
            }
            else
            {
                return NotFound(id);

            }
        }
    }
}