namespace Day8Lab1.Models
{
    public class Anagraphic
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        /*public string FullName
        {
            get { $"{Name} {Surname}" }
        }*/
        public Anagraphic() 
        {
            Name= string.Empty;
            Surname= string.Empty;
            PhoneNumber= string.Empty;
            Email= string.Empty;
        }

        public override string ToString()
        {
            return $"{Name} {Surname} ==> {PhoneNumber} {Email}";
        }
    }

    public class Rubrica : List<Anagraphic> { }
}
