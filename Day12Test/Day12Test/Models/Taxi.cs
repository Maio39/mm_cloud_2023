namespace Day12Test.Models
{
    public class Taxi
    {
        public int TaxiId { get; set; }
        public string TaxiName { get; set;}
        public bool IsBusy { get; set; }
        public bool IsActive { get; set; }
        public List<Race> TaxiRaces { get; set; }

        public Taxi() 
        {
            TaxiName = string.Empty;
            IsBusy = false;
            IsActive = true;
            TaxiRaces = new List<Race>();
        }
    }
}
