using System.Runtime.CompilerServices;

namespace GiocoImpiccato
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<Char>> utenti = new Dictionary<string, List<Char>>();
            string key;
            char tentative;
            string str_tentative;
            int numeroTentativi = 10;
            int count = 1;

            while (count != 0)
            {
                Console.WriteLine("Gioco dell'Impiccato");
                Console.WriteLine("Menu:");
                Console.WriteLine("1 - New Game");
                Console.WriteLine("2 - Vedi tutti i game che ci sono stati");
                Console.WriteLine("0 - Esc");

                string str_opt = Console.ReadLine();

                int option = Int32.Parse(str_opt);
                switch (option)
                {
                    case 1:
                        List<Char> Tentativi = new List<Char>();
                        Console.WriteLine("Inserisci la parola da indovinare: ");
                        do
                        {
                            key = Console.ReadLine();
                        } while (!Impiccato.IsValid(key));

                        bool[] caratteriIndovinati = new bool[key.Length];

                        string str_result = "";

                        Console.WriteLine($"La parola da indovinare ha {key.Length} lettere");
                        
                        while (numeroTentativi > 0 && Array.IndexOf(caratteriIndovinati, false) != -1)
                        {
                            Console.WriteLine($"Hai a disposizione {numeroTentativi} tentativi");
                            Console.WriteLine($"Parola: {Impiccato.GetLettereNascoste(key,caratteriIndovinati)}");
                            do
                            {
                                Console.WriteLine("Inserisci il carattere che pensi sia nella parola!");
                                str_tentative = Console.ReadLine();

                            } while (Impiccato.IsValid(str_tentative) && str_tentative.Length != 1);

                            char[] charArray = str_tentative.ToCharArray();
                            tentative = charArray[0];
                            Tentativi.Add(tentative);

                            bool foundLetter = false;
                            for (int j = 0; j < key.Length; j++)
                            {
                                if (key[j] == tentative)
                                {
                                    caratteriIndovinati[j] = true;
                                    foundLetter = true;
                                }
                            }

                            if (foundLetter)
                            {
                                Console.WriteLine($"La lettera '{tentative}' è presente nella parola!");
                            }
                            else
                            {
                                Console.WriteLine($"La lettera '{tentative}' non è presente nella parola.");
                                numeroTentativi--;
                            }
                        }
                        utenti.Add(key, Tentativi);
                        if (numeroTentativi == 0)
                        {
                            Console.WriteLine($"Hai esaurito i tentativi. La parola era '{key}'.");
                        }
                        else
                        {
                            Console.WriteLine($"Hai indovinato! La parola era '{key}'.");
                        }

                        Console.WriteLine("Premi un tasto per uscire.");
                        Console.ReadLine();
                        break;
                    case 2:
                        int contaTentativi = 0;
                        int contaUtenti = 0;
                        foreach (KeyValuePair<string, List<Char>> _kvp in utenti)
                        {
                            contaUtenti++;
                            Console.WriteLine("\nUtente " + contaUtenti);
                            Console.WriteLine("Chiave: " + _kvp.Key);
                            foreach (char c in _kvp.Value)
                            {
                                contaTentativi++;
                                Console.WriteLine("n" + contaTentativi + ": " + c + "\n");
                            }

                        }
                        break;
                    case 0:
                        count = 0;
                        break;
                }
            }

        }

        public class Impiccato
        {
            static char[] caratteriValidi = { 'a', 'b', 'c', 'd', 'e', 'f','g','h','i','j','k','l','m',
                                              'n','o','p','q','r','s','t','u','v','z','y','w','x' };

            public static bool IsValid(string sequence)
            {
                foreach (char c in sequence)
                {
                    if (!caratteriValidi.Contains(c))
                    {
                        return false;
                    }
                }
                return true;
            }

            public static string GetLettereNascoste(string key, bool[] lettereIndovinate)
            {
                string letteraNascosta = "";
                for (int i = 0; i < key.Length; i++)
                {
                    if (lettereIndovinate[i])
                    {
                        letteraNascosta += key[i];
                    }
                    else
                    {
                        letteraNascosta += "_";
                    }
                }

                return letteraNascosta;
            }

        }
    }
}