using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Day4Lab1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string[] _data { get; set; }

        //public Anagrafica[] anagraficas { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            //_data = new string[0];
        }
        public void OnGet()
        {
            _data = "Ma la volpe col suo balzo ha raggiunto quel fido".Split(' ');
            List<Anagrafica> elenco = new List<Anagrafica>();
            for (int i=0,j=5; i<5; i++,j--)
            {
                elenco.Add(new Anagrafica()
                {
                    FirstName = $"Antani {i}",
                    LastName = $"Sbriguda {j}"
                });
                
                /*
                anagraficas = new Anagrafica[5];
                anagraficas[i] = new Anagrafica();
                anagraficas[i].FirstName = $"Gianni {i}";
                anagraficas[i].LastName = $"Morandi {i}";
                */
            }
            ViewData["max"] = elenco;


        }
    }
}