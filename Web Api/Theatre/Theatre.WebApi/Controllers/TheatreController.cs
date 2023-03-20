using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Theatre.WebApi.Controllers
{
    public class TheatreTickets
    {
        public int Id;
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BoughtNumber { get; set; }
        public int SpentMoney { get; set; }


    }
    public class TheatreController : ApiController
    {
        public List<TheatreTickets> Customers = new List<TheatreTickets>
        {
            new TheatreTickets { Id = 1, Name = "Ivan", Surname = "Ivic", BoughtNumber = 5, SpentMoney = 50 },
            new TheatreTickets { Id = 2, Name = "Josip", Surname = "Josic", BoughtNumber = 6, SpentMoney = 60 },
            new TheatreTickets { Id = 3, Name = "Luka", Surname = "Vucemilovic", BoughtNumber = 7, SpentMoney = 70 },
            new TheatreTickets { Id = 4, Name = "Krunoslav", Surname = "Skoro", BoughtNumber = 2, SpentMoney = 10 },
            new TheatreTickets { Id = 5, Name = "Jusuf", Surname = "Begovic", BoughtNumber = 3, SpentMoney = 30 }
        };


        // GET api/values
        public IEnumerable<TheatreTickets> Get()
        {
            return Customers;
        }

        // GET api/Theatre/Number Desired
        public TheatreTickets Get(int id)
        {
            return Customers.FirstOrDefault(x => x.Id == id);
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
            {
                // Generate a new ID for the customer by finding the maximum ID in the list and adding 1
                int newId = Customers.Max(x => x.Id) + 1;

                // Set the ID property of the new customer
                customer.Id = newId;

                // Add the new customer to the list
                Customers.Add(customer);
            }
        }

        // PUT api/Theatre/5
        public void Put(int id, [FromBody] TheatreTickets customer)
        {
            int index = Customers.FindIndex(x => x.Id == id);
            if (index >= 0)
            {
                Customers[index] = customer;
            }
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            TheatreTickets customerToRemove = Customers.FirstOrDefault(x => x.Id == id);
            if (customerToRemove != null)
            {
                Customers.Remove(customerToRemove);
            }
        }
    }
}
