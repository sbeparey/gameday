using System.Collections.Generic;
using Gameday.DotNet.Web.Data;
using Gameday.DotNet.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gameday.DotNet.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomersController : Controller
    {
        private const string connectionString = "host=localhost;database=gameday;username=gameday;password=lets play;";
        private readonly CustomersData customersData;

        public CustomersController()
        {
            customersData = new CustomersData(connectionString);
        }

        // GET
        public IEnumerable<Customer> Index()
        {
            return customersData.GetCustomers();
        }

        [HttpPut("{id}")]
        public bool PutCustomer(long id, [FromBody] MiniCustomer miniCustomer)
        {
            return customersData.UpdateCustomer(id, miniCustomer.FirstName, miniCustomer.LastName, miniCustomer.Phone);
        }
    }
}