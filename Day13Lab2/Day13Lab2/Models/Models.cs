using System.ComponentModel.DataAnnotations.Schema;

namespace Day13Lab2.Models
{
    public class Zone
    {
        public int ZoneID { get; set; }
        public string Description { get; set; } = "";
        public bool IsActive { get; set; }

        public Single TargetTemperature { get; set; }
        [NotMapped]
        public bool IsAlarm { get {
                int deltaT = 1;
                var x = from t in Rilevazioni
                        where t.TemperatureValue > TargetTemperature +deltaT || t.TemperatureValue < TargetTemperature-deltaT
                        select t;
                return x.Count()!=0; 
            } }
        public List<Temperature> Rilevazioni { get; set; } = new List<Temperature>();
        public int HumidityID {get; set; }
        public Humidity Humidity { get; set; } = new Humidity();


    }

    public class Temperature
    {
        public int TemperatureID { get; set; }
        public DateTime TemperatureDate { get; set; }
        public Single TemperatureValue { get; set; }
        public int ZoneID { get; set; }
        public Zone Zone { get; set; } = new Zone();

        public override string ToString()
        {
            return $"{TemperatureValue}°C";
        }
    }

    public class Humidity
    {
        public int HumidityID { get; set; }
        public float Value { get; set; }
        public string Unit { get; set; } = "";
        public List<Zone> Zones { get; set; } = new List<Zone>();

        public override string ToString()
        {
            return $"{Value} {Unit}";
        }

    }
}
