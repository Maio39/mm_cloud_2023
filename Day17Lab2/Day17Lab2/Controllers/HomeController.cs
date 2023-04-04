using Day17Lab2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Day17Lab2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        CodiceFiscale codiceFiscale = null;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult GeneraCF() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult GeneraCF([FromForm]string Cognome,string Nome,string Sesso,DateTime Nascita,string Comune,string provincia,int LivelloOmocodia)
        {
            CodiceFiscale cod = new CodiceFiscale(Cognome, Nome, Sesso, Nascita, Comune, provincia, LivelloOmocodia);
            return RedirectToAction(nameof(ViewCF),cod);
        }

        public IActionResult ViewCF(CodiceFiscale cf)
        {
            return View(cf);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}