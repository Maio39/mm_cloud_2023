using Day19Lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Day19Lab1.Controllers
{
    public class CustomerController : Controller
    {
        private readonly DataContext _context;
        public CustomerController(DataContext _DContext) 
        {
            _context = _DContext;
            //Popolate DB
            if(_context.Customers.Count()==0) { 
                for(int i=0;i<10;i++)
                {
                    Customer mio = new Customer()
                    {
                        CustomerStatus = 1,
                        CustomerName = $"Customer {i}",
                        CustomerEmail = $"cust{i}@test.com",
                        CustomerPhone = $"555-5{i}9-9338"
                    };
                    _context.Customers.Add(mio);
                }
                _context.SaveChanges();
            }
        }
        public IActionResult Index()
        {
            var custs = _context.Customers.ToList();
            return View(custs);
        }
    }
}
