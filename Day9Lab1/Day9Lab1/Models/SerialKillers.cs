using System.IO;

namespace Day9Lab1.Models
{
    public class SerialKillers
    {
        public List<string> Killers = new List<string>();
        public SerialKillers() 
        {
            LoadFromFile();
        }

        public void LoadFromFile(string path = "names.txt")
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
            //Second Way
            //Killers = new List<string>(File.ReadAllLines(path));
        }

        public List<string> GetKillerByLen(int KillerLen=5)
        {
            /* Manual Way
            List <string> list = new List<string>();
            foreach(string killer in Killers)
            {
                if(killer.Length == KillerLen)
                {
                    list.Add(killer);
                }
            }
            return list; */

            /* var list = from killer in Killers
                       where killer.Length == KillerLen
                       //orderby killer
                       select killer;
            return list.ToList();*/

            /* Query Syntax */
            return (from killer in Killers
                    where killer.Length == KillerLen
                    //orderby killer
                    select killer).ToList();

            /*Method Syntax
            return Killers
                .Where(s => s.Length == KillerLen)
                //.OrderBy(killer=>killer.Lenght)
                .ToList();*/

        }

        public List<string> GetKillerByStr(string str)
        {
            return (from killer in Killers
                    where killer.ToLower().StartsWith(str.ToLower())
                    select killer).ToList();
        }
    }
}
