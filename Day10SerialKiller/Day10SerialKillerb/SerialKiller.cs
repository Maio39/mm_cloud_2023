using System.IO;

namespace Day10SerialKillerb
{
    public class SerialKiller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
        public int Killing { get; set; }
        public bool IsInJail { get; set; }


        public SerialKiller() 
        {
            Name = string.Empty;
            Surname = string.Empty;
            Description = string.Empty;
            Killing = 0;
            IsInJail = false;
        }

        /*/
        public void LoadFromFile(string path = "middle-names.txt")
        {
            if(File.Exists(path))
            {
                Killers = new List<string>(File.ReadAllLines(path));
            }
            else
            {
                Killers = new List<string>();
                throw new ArgumentException("File Doesn't exists");
            }
        }
        //*/

        /*/
        public SerialKiller GetRandomKiller()
        {
            string path = "middle-names.txt";
            string path2 = "names.txt";
            if (File.Exists(path) && File.Exists(path2))
            {
                List<string> Surname = new List<string>(File.ReadAllLines(path));
                List<string> Name = new List<string>(File.ReadAllLines(path2));
                int indexSurname = new Random().Next(0, Surname.Count);
                int indexName = new Random().Next(0, Name.Count);
                return new SerialKiller()
                {
                    Name = Name[indexName],
                    Surname = Surname[indexSurname],
                    Description = $"Sono il killer {this.Name} {this.Surname}",
                    Killing = new Random().Next(0,20),
                    IsInJail = new Random().NextInt64()%2==0
                };
            }
            else
            {
                return null;
                throw new ArgumentException("Files Doesn't exists");
            }
        }
        //*/
    }
}