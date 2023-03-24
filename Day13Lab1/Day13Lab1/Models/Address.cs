using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Day13Lab1.Models
{
    public class Address
    {
        public int AddressID { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string AddressText { get; set; }
        public int MyAnagraphicAnagraphicID { get; set; } //con nome dell anagraphic dentro l'address e id dell'anagraphic dentro anagraphic 
        [ValidateNever]
        public Anagraphic MyAnagraphic { get; set; }
    }
}
