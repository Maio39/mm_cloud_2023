using Day8Lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Day8Lab1.Controllers
{
    public class RubricaController : Controller
    {
        private readonly Rubrica _rub;
        public RubricaController(Rubrica rubrica) 
        {
            _rub = rubrica;
            //Empty; full
            if(_rub.Count == 0)
            {
                for(int i = 0; i < 5; i++)
                {
                    _rub.Add(new Anagraphic()
                    {
                        Name = $"Firstname {i}",
                        Surname = $"LastName{i}",
                        PhoneNumber = $"555-435-333-{i}",
                        Email = $"test_{i}@gmail.com"
                    });
                }
            }
        }
        public IActionResult Index()
        {
            return View(_rub);
        }
        public IActionResult Update(int? antani)
        {
            if(antani == null || antani>=_rub.Count)
            {
                return RedirectToAction("index");
            }
            return View("Views/Rubrica/Anagrafica.cshtml", _rub[(int)antani]);
        }

        [HttpPost]
        public IActionResult Update(int? antani, [FromForm] Anagraphic updated)
        {
            if (antani == null || antani >= _rub.Count || updated == null)
            {
                return RedirectToAction("index");
            }
            try
            {
                _rub[(int)antani] = updated;
            } catch (Exception ex)
            {
                //
            }
            return RedirectToAction("index");
        }

        
        /* Eliminare chidendo conferma tramite javascript
        public IActionResult Delete(int? indexContact)
        {
            if (indexContact == null || indexContact >= _rub.Count)
            {
                return RedirectToAction("index");
            }
            _rub.RemoveAt((int)indexContact);
            return RedirectToAction("index");
        }*/

        public IActionResult Delete(int? indexContact)
        {
            if (indexContact == null || indexContact >= _rub.Count)
            {
                return RedirectToAction("index");
            }
            
            return View("Views/Rubrica/Delete.cshtml", _rub[(int)indexContact]);
        }

        [HttpPost]
        public IActionResult Delete(int? indexContact, [FromForm] Anagraphic deleted)
        {
            if (indexContact == null || indexContact >= _rub.Count || deleted == null)
            {
                return RedirectToAction("index");
            }
            try
            {
                _rub.RemoveAt((int)indexContact);
            }
            catch (Exception ex)
            {
                //
            }
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult New()
        {
            return View("Views/Rubrica/Anagrafica.cshtml", new Anagraphic());
        }

        [HttpPost]
        public IActionResult New([FromForm] Anagraphic Nuova)
        {
            _rub.Add(Nuova);
            return RedirectToAction("index");
        }
        
        
    }
}
