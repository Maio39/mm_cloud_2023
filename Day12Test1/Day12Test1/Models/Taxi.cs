using Microsoft.AspNetCore.Mvc;

namespace Day12Test1.Models
{
    public class Taxi
    {
        [BindProperty]
        public int TaxiId { get; set; }
        [BindProperty]
        public string TaxiName { get; set;}
        [BindProperty]
        public bool IsBusy { get; set; }
        [BindProperty]
        public bool IsActive { get; set; }
        public List<Race> Races { get; set; }

        public Taxi() 
        {
            TaxiName = string.Empty;
            IsBusy = false;
            IsActive = true;
            Races = new List<Race>();
        }
    }
}
