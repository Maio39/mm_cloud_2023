using Microsoft.AspNetCore.Mvc;

namespace Day8Lab1.Controllers
{
    public class CalcController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sum(decimal? a,decimal? b)
        {
            ViewData["result"] = a + b;
            return View("Views/Calc/index.cshtml");
        }

        public IActionResult Substraction(decimal? a, decimal? b) 
        {
            ViewData["result"] = a - b;
            return View("Views/Calc/index.cshtml");
        }

        public IActionResult Multiplication(decimal? a, decimal? b) 
        {
            ViewData["result"] = a * b;
            return View("Views/Calc/index.cshtml");
        }

        public IActionResult Division(decimal? a, decimal? b)
        {
            ViewData["result"] = a / b;
            return View("Views/Calc/index.cshtml");
        }
    }
}
