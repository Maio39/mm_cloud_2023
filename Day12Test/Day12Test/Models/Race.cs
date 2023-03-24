using System.ComponentModel.DataAnnotations.Schema;

namespace Day12Test.Models
{
    public class Race
    {
        public int RaceId {get; set;}
        [ForeignKey("TaxiId")]
        public int TaxiId { get; set;}
        public string RaceName { get; set;}
        public string RaceDestination { get; set;}
        public decimal RacePrice { get;}
        public decimal KmRace { get; set; }
        public DateTime RaceDate { get; set;}

        public Race()
        {
            RaceName=RaceDestination = string.Empty;
            RacePrice = (decimal)5*KmRace;
            KmRace = 0;
            RaceDate = DateTime.Now;
        }
    }
}
