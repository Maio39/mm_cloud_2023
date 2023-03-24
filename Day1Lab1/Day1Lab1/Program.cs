using Day1Lab1.Models;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Day1Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            List<Contatto> listaContatti = new List<Contatto>();
            
            for(int i=0; i<4; i++)
            {
                Contatto contatto1 = new Contatto();
                Console.WriteLine($"Contatto {i}: ");
                Console.WriteLine("Inserisci il nome");
                contatto1.Name = Console.ReadLine();
                Console.WriteLine("Inserisci il cognome");
                contatto1.Surname = Console.ReadLine();
                Console.WriteLine("Inserisci il numero di telefono");
                contatto1.PhoneNumber = Console.ReadLine();
                Console.WriteLine("\n");
                listaContatti.Add(contatto1);
            }

            foreach (Contatto contatto in listaContatti)
            {
                Console.WriteLine(contatto);
            }
            */

            /*string[] NameList = { "Arnold", "Franco", "Giovanni",
                                  "Asia", "Giacomo"};
            string[] SurnameList = { "Rossi", "Verdi",
                                    "\"Bomber\"",};
            */
            Rubrica AmiciMiei = new Rubrica();
            for(int i = 0; i<10;i++)
            {
                
                /*old1
                Contatto contatto = new Contatto();
                //contatto.Name = $"Gianni_{i}";
                contatto.Name = NameList[i%NameList.Length];
                contatto.Surname = SurnameList[i % SurnameList.Length];
                contatto.PhoneNumber = $"327463746{i}";
                

                old2
                Contatto contatto = new EmailContatto()
                {
                    Name = NameList[i % NameList.Length],
                    Surname = SurnameList[i % SurnameList.Length],
                    PhoneNumber = $"327463746{i}",
                    Email = $"Antani:{i}"
                };*/
                
                Contatto c = Contatto.GetRandomContact();
                AmiciMiei.AddContatto(c);
            }
            int count = 1;

            while(count != 0)
            {
                Console.WriteLine("Menu Rubrica");
                Console.WriteLine("1 - Inserisci Nuovo Contatto");
                Console.WriteLine("2 - Elencare tutti i Contatti");
                Console.WriteLine("3 - Cercare un Contatto");
                Console.WriteLine("4 - Elimina un contatto dalla lista");
                Console.WriteLine("5 - Modifica un contatto");
                Console.WriteLine("6 - Visualizza tutto il dizionario");
                Console.WriteLine("7 - Elimina un Contatto nel dizionario");
                Console.WriteLine("0 - Esci\n");
                Console.WriteLine("Inserisci l\'opzione desiderata: ");
                string str_option = Console.ReadLine();
                int option = Int32.Parse(str_option);
                switch (option)
                {
                    case 1:
                        if (InserisciContatto())
                        {
                            Console.WriteLine("Contatto inserito correttamente");
                        }
                        else
                        {
                            Console.WriteLine("Errore input non valido");
                        }
                    break;
                    case 2:
                        VisuallizzaContatti();    
                    break;
                    case 3:
                        Console.WriteLine("Scegliere il tipo di ricerca da fare: ");
                        Console.WriteLine("1 - Ricerca per nome");
                        Console.WriteLine("2 - Ricerca per cognome");
                        Console.WriteLine("3 - Ricerca per numero di telefono");
                        Console.WriteLine("4 - Ricerca per Nome e Cognome");
                        string str_opt = Console.ReadLine();
                        int opt = Int32.Parse(str_opt);
                        Search(opt);
                        
                    break;
                    case 4:
                        VisualizzaContattiWithIndex();
                        Console.WriteLine("Inserisci Il numero del contatto da eliminare");
                        string str_index_elimina_contatto = Console.ReadLine();
                        int index_contatto_elimina = Int32.Parse(str_index_elimina_contatto);
                        if (EliminaContatto(index_contatto_elimina))
                        {
                            Console.WriteLine("\nContatto eliminato correttamente\n");
                            Console.WriteLine("\nRubrica Contatti Aggiornata: \n");
                            VisualizzaContattiWithIndex();
                        }
                        else
                        {
                            Console.WriteLine("ERROR!! - Contatto non eliminato");
                        }



                        break;
                    case 5:
                        VisualizzaContattiWithIndex();
                        Console.WriteLine("\nInserisci Il numero del contatto da modificare");
                        string str_index_contatto_modifica = Console.ReadLine();
                        int index_contatto_modifica = Int32.Parse(str_index_contatto_modifica);

                        if (index_contatto_modifica >= 0 && index_contatto_modifica < AmiciMiei.ListaContatti().Count)
                        {
                            Console.WriteLine("\nQuale campo vuoi modificare? ");
                            Console.WriteLine("1 - Name");
                            Console.WriteLine("2 - Surname");
                            Console.WriteLine("3 - PhoneNumber");
                            string str_option_modifica = Console.ReadLine();
                            int option_modifica = Int32.Parse(str_option_modifica);
                            if (ModificaContatto(option_modifica, index_contatto_modifica))
                            {
                                Console.WriteLine("\nContatto modificato correttamente\n");
                                Console.WriteLine("\nRubrica Contatti Aggiornata: \n");
                                VisualizzaContattiWithIndex();
                            }
                            else
                            {
                                Console.WriteLine("ERROR!! - Contatto non modificato");
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("Non è stato possibile modificare il contatto");
                        }
                        break;
                    case 6:
                        GetFullDictionaryContact();
                    break;
                    case 7:
                        GetFullDictionaryContact();
                        Console.WriteLine("Inserisci Il nome e il cognome del contatto da eliminare");
                        string name_surname_elimina_contatto = Console.ReadLine();
                        AmiciMiei.DeleteContatctByObject(AmiciMiei.GetContattoByFullName(name_surname_elimina_contatto));
                    break;
                    case 0:
                        count = 0;
                    break;
                    

                }
            }
            bool InserisciContatto()
            {
                Contatto contatto1 = new Contatto();
                Console.WriteLine("Inserisci il nome");
                contatto1.Name = Console.ReadLine();
                Console.WriteLine("Inserisci il cognome");
                contatto1.Surname = Console.ReadLine();
                Console.WriteLine("Inserisci il numero di telefono");
                contatto1.PhoneNumber = Console.ReadLine();
                Console.WriteLine("\n");
                if(!string.IsNullOrWhiteSpace(contatto1.Name) && !string.IsNullOrWhiteSpace(contatto1.Surname) && !string.IsNullOrWhiteSpace(contatto1.PhoneNumber))
                {
                    if (AmiciMiei.AddContatto(contatto1))
                    {
                        return true;
                    }
                    else 
                    {
                        Console.WriteLine("Impossibile aggiungere contatto");
                        Console.WriteLine("Questo contatto è già presente in rubrica");
                        return false; 
                    }
                }
                else
                {
                    return false;
                }
            }

            void GetFullDictionaryContact()
            {
                foreach (KeyValuePair<string, Contatto> _kvp in AmiciMiei.DictionaryContatti())
                {
                    Console.WriteLine("Chiave: " + _kvp.Key + ", Valore: " + _kvp.Value);
                }
            }

            void GetFullDictionaryContactWithIndex()
            {
                int _count = 0;
                foreach (KeyValuePair<string, Contatto> _kvp in AmiciMiei.DictionaryContatti())
                {
                    Console.WriteLine($"({_count}) Chiave: " + _kvp.Key + ", Valore: " + _kvp.Value);
                    _count++;
                }
            }

            void VisuallizzaContatti()
            {
                foreach (Contatto c_iter in AmiciMiei.ListaContatti())
                {
                    Console.WriteLine(c_iter);
                }
            }
            
            void VisualizzaContattiWithIndex()
            {
                foreach (Contatto cc in AmiciMiei.ListaContatti())
                {
                    Console.WriteLine($"\n{AmiciMiei.ListaContatti().IndexOf(cc)} {cc.ToString()}");
                }
                Console.WriteLine("\n");
            }

            bool EliminaContatto(int index_contatto_elimina)
            {
                if (AmiciMiei.DeleteContactByIndex(index_contatto_elimina))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            bool ModificaContatto(int option_modifica, int index_contatto_modifica)
            {
                if (option_modifica > 0 && option_modifica < 4)
                {
                    switch (option_modifica)
                    {
                        case 1:
                            Console.WriteLine("Inserisci il nome modificato");
                            string name_modify = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(name_modify))
                            {
                                AmiciMiei.EditContactByIndex(index_contatto_modifica, name_modify, option_modifica);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                            break;
                        case 2:
                            Console.WriteLine("Inserisci il cognome modificato");
                            string surname_modify = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(surname_modify))
                            {
                                AmiciMiei.EditContactByIndex(index_contatto_modifica, surname_modify, option_modifica);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                            break;
                        case 3:
                            Console.WriteLine("Inserisci il phone Number modificato");
                            string phonenumber_modify = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(phonenumber_modify))
                            {
                                AmiciMiei.EditContactByIndex(index_contatto_modifica, phonenumber_modify, option_modifica);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                            break;
                        default: return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            void Search(int opt)
            {
                if(opt >= 0 && opt < 6)
                {
                    switch (opt)
                    {
                        case 1:
                            Console.WriteLine("Inserisci il nome del contatto da cercare: ");
                            string nome = Console.ReadLine();
                            foreach (Contatto c_name_iter in AmiciMiei.ListaContatti())
                            {
                                if (c_name_iter.Name.Equals(nome))
                                {
                                    Console.WriteLine(c_name_iter);
                                }
                                else
                                {
                                    Console.WriteLine("Nessun contatto trovato con questo nome");
                                }
                            }
                            break;
                        case 2:
                            Console.WriteLine("Inserisci il cognome del contatto da cercare: ");
                            string cognome = Console.ReadLine();
                            foreach (Contatto c_name_iter in AmiciMiei.ListaContatti())
                            {
                                if (c_name_iter.Surname.Equals(cognome))
                                {
                                    Console.WriteLine(c_name_iter);
                                }
                                else
                                {
                                    Console.WriteLine("Nessun contatto trovato con questo cognome");
                                }
                            }
                            break;
                        case 3:
                            Console.WriteLine("Inserisci il numero del contatto da cercare: ");
                            string numero = Console.ReadLine();
                            foreach (Contatto c_name_iter in AmiciMiei.ListaContatti())
                            {
                                if (c_name_iter.PhoneNumber.Equals(numero))
                                {
                                    Console.WriteLine(c_name_iter);
                                }
                                else
                                {
                                    Console.WriteLine("Nessun contatto trovato con questo numero di telefono");
                                }
                            }
                            break;
                        case 4:
                            Console.WriteLine("Inserisci il nome del contatto da cercare: ");
                            string nome2 = Console.ReadLine();
                            Console.WriteLine("Inserisci il cognome del contatto da cercare: ");
                            string cognome2 = Console.ReadLine();
                            foreach (Contatto c_name_iter in AmiciMiei.ListaContatti())
                            {
                                if (c_name_iter.Name.Equals(nome2))
                                {
                                    if (c_name_iter.Surname.Equals(cognome2))
                                    {
                                        Console.WriteLine(c_name_iter);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Nessun contatto trovato con questo nome e cognome");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Nessun contatto trovato con questo nome e cognome");
                                }
                            }
                            break;
                    }
                }
            }
        }
    }
}