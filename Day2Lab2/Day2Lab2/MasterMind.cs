namespace Day2Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int count=1;
            Dictionary<string, List<string>> utenti = new Dictionary<string, List<string>>();
            while (count != 0)
            {
                Console.WriteLine("MasterMind!");
                Console.WriteLine("Menu:");
                Console.WriteLine("1 - New Game inserendo manualmente la chiave");
                Console.WriteLine("2 - New Game generando casualmente la chiave");
                Console.WriteLine("3 - Vedi tutti i game che ci sono stati"); 
                Console.WriteLine("0 - Esc");

                string str_opt = Console.ReadLine();

                int option = Int32.Parse(str_opt);
                switch (option)
                {
                    case 1:
                        string key;
                        string tentative;
                        List<string> Tentativi = new List<string>();
                        Console.WriteLine("Possibile Color: R = Rosso G = Giallo V = Verde B = Bianco N = Nero A = Azzurro ");
                        Console.WriteLine("Please Insert Key:");
                        do
                        {
                            key = Console.ReadLine();
                        } while (!MasterMind.IsValid(key));

                        for (int i = 1; i < 11; i++)
                        {
                            Console.WriteLine($"Tentativo n {i}");
                            tentative = Console.ReadLine();
                            MasterMind.Indizi(tentative, key);
                            if (MasterMind.IsValid(tentative))
                            {
                                Tentativi.Add(tentative);
                                if (tentative == key)
                                {
                                    Console.WriteLine("Hai Vintoo!!");
                                    utenti.Add(key, Tentativi);
                                    break;
                                }
                            }
                        }
                        break;
                    case 2:
                        string casualKey = MasterMind.GenerateKey();
                        string tentative2;
                        List<string> Tentativi2 = new List<string>();
                        Console.WriteLine("Possibile Color: R = Rosso G = Giallo V = Verde B = Bianco N = Nero A = Azzurro ");
                        for (int i = 1; i < 11; i++)
                        {
                            Console.WriteLine($"Tentativo n {i}");
                            tentative2 = Console.ReadLine();
                            MasterMind.Indizi(tentative2, casualKey);
                            if (MasterMind.IsValid(tentative2))
                            {
                                Tentativi2.Add(tentative2);
                                if (tentative2 == casualKey)
                                {
                                    Console.WriteLine("Hai Vintoo!!");
                                    utenti.Add(casualKey, Tentativi2);
                                    break;
                                }
                            }
                        }

                        break;
                    case 3:
                        int contaTentativi = 0;
                        int contaUtenti = 0;
                        foreach(KeyValuePair<string,List<string>> _kvp in utenti)
                        {
                            contaUtenti++;
                            Console.WriteLine("\nUtente " + contaUtenti);
                            Console.WriteLine("Chiave: " + _kvp.Key);
                            foreach(string str in _kvp.Value)
                            {
                                contaTentativi++;
                                Console.WriteLine("n"+ contaTentativi + ": " + str + "\n");
                            }
                            
                        }
                        break;
                    case 0:
                        count = 0;
                        break;
                }

            }
            
        }

        public class MasterMind
        {
            static char[] caratteriValidi = { 'R', 'G', 'V', 'B', 'N', 'A'};

            public static bool IsValid(string sequence)
            {
                if (sequence.Length > 4)
                {
                    return false;
                }
                foreach (char c in sequence)
                {
                    if (!caratteriValidi.Contains(c))
                    {
                        return false;
                    }
                }
                return true;
            }

            public static string GenerateKey()
            {
                Random rand = new Random();

                string sequence = "";
                for (int i = 0; i < 4; i++)
                {
                    int index = rand.Next(0, 6);
                    sequence += caratteriValidi[index];
                }
                return sequence;
            } 

            public static void Indizi(string tentative, string key)
            {
                IEnumerable<Char> SequenceTentative = tentative.Distinct();
                IEnumerable<Char> SequenceKey = key.Distinct();
                int i = 0;

                foreach(Char c in SequenceKey)
                {
                    i++;
                    int j = 0;
                    foreach (Char c2 in SequenceTentative)
                    {
                        j++;
                        if (c == c2)
                        {
                            if (i==j)
                            {
                                Console.WriteLine("Colore giusto al posto giusto");
                            }
                            else
                            {
                                Console.WriteLine("Colore giusto al posto sbagliato");
                            }
                        }
                    }
                }
            }
        }
    }
}