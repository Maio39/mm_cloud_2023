using Day9Lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Day9Lab1.Controllers
{
    public class KillerController : Controller
    {
        public SerialKillers SerialKillers { get; set; }
        public KillerController() 
        {
            SerialKillers = new SerialKillers();
        }
        public IActionResult Index()
        {
            return View(new List<string>());
        }

        [HttpPost]
        public IActionResult SearchKiller(string search, int numKiller=1000)
        {
            if (search != null)
            {
                return View("Views/Killer/Index.cshtml", SerialKillers.GetKillerByStr(search).Take((int)numKiller));
            }
            else
            {
                return View("Views/Killer/Index.cshtml",SerialKillers.Killers.Take((int)numKiller));
            }
            
        }
    }
}
