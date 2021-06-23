using System.Collections.Generic;
using Gameday.DotNet.Web.Data;
using Gameday.DotNet.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gameday.DotNet.Web.Controllers
{
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
            // var sw = new Stopwatch();
            //
            // sw.Start();
            return customersData.GetCustomers();
            // sw.Stop();
            
            // Response.Headers.Add("ElaspedTime", sw.ElapsedMilliseconds.ToString());
            // return customers;
        }

        public bool Put(long id, [FromBody] MiniCustomer miniCustomer)
        {
            return customersData.UpdateCustomer(id, miniCustomer.FirstName, miniCustomer.LastName, miniCustomer.Phone);
        }
    }
}