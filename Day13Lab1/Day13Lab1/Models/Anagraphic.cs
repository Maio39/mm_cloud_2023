namespace Day13Lab1.Models
{
    public class Anagraphic
    {
        public int AnagraphicID { get; set; }
        public string CompanyName { get; set; }
        public string VAT { get; set; }
        public List<Address> AddressList { get; set; } = new List<Address>();
    }
}
