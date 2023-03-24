using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace Day6Lab1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public ViewLingo LingoGame;

        public string WordTentative { get; set; }

        public string LastTentative { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ViewLingo vf)
        {
            _logger = logger;
            LingoGame = vf;
            WordTentative = Lingo.GetOscurateWord(LingoGame.Key,LingoGame.guessedLetters);
            LastTentative = LingoGame.Tentative.LastOrDefault();
        }

        public void OnGet()
        {
            LingoGame.newGame();
            LastTentative = LingoGame.Tentative.LastOrDefault();
            WordTentative = Lingo.GetOscurateWord(LingoGame.Key, LingoGame.guessedLetters);
        }

        public void OnPost(string word)
        {
            if(word!=null && word!="" && word.Length == LingoGame.Key.Length) {
                WordTentative = word;
                //Console.WriteLine(word);
                LingoGame.Tentative.Add(word);
                LingoGame.clues(word);
                //LastTentative = WordTentative;
                
            }
        }
    }
}