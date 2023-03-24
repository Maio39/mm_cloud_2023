using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Day1Lab1.Models
{
    public class Rubrica
    {
        protected List<Contatto> _elenco;
        protected Dictionary<string, Contatto> _dictio;
        public Rubrica() 
        {
            _elenco = new List<Contatto>();
            _dictio = new Dictionary<string, Contatto>();
            Console.WriteLine("Creato una rubrica");
        }

        public bool AddContatto(Contatto contatto)
        {
            int count = 0;
            foreach (Contatto iter_contact in _elenco)
            {
                if (iter_contact.Name.Equals(contatto.Name))
                {
                    if (iter_contact.Surname.Equals(contatto.Surname))
                    {
                        if(iter_contact.PhoneNumber.Equals(contatto.PhoneNumber))
                        {
                            count++;
                        }
                    }
                }
            }
            if(count == 0)
            {
                _elenco.Add(contatto);
                _dictio[contatto.FullName] = contatto;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddContattoDict(Contatto contatto)
        {
            if (_dictio.ContainsKey(contatto.Name))
            {
                _dictio[contatto.FullName] = contatto;
            }
        }

        public Contatto GetContattoByFullName(string fName) 
        {
            return _dictio[fName];
        }

        public void DeleteContatctByObject(Contatto _contact)
        {
            if (_dictio.Remove(_contact.FullName))
            {
                Console.WriteLine("Contatto eliminato correttamente");
            }
            else
            {
                Console.WriteLine("Contatto non eliminato");
            }
        }

        public bool DeleteContactByIndex(int index_delete)
        {
            if (index_delete <= _elenco.Count && index_delete >= 0)
            {
                _elenco.RemoveAt(index_delete);
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool EditContactByIndex(int index_modify_contact, string modifica, int option)
        {
            switch(option)
            {
                case 1:
                    if (index_modify_contact < _elenco.Count && index_modify_contact >= 0)
                    {
                        Contatto c = _elenco[index_modify_contact];
                        c.Name = modifica;
                        _elenco.Insert(index_modify_contact, c);
                        _elenco.RemoveAt(index_modify_contact+1);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 2:
                    if (index_modify_contact < _elenco.Count && index_modify_contact >= 0)
                    {
                        Contatto c = _elenco[index_modify_contact];
                        c.Surname = modifica;
                        _elenco.Insert(index_modify_contact, c);
                        _elenco.RemoveAt(index_modify_contact + 1);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 3:
                    if (index_modify_contact < _elenco.Count && index_modify_contact >= 0)
                    {
                        Contatto c = _elenco[index_modify_contact];
                        c.PhoneNumber = modifica;
                        _elenco.Insert(index_modify_contact, c);
                        _elenco.RemoveAt(index_modify_contact + 1);
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

        public List<Contatto> ListaContatti()
        {
            return _elenco;
        }



        public Dictionary<string, Contatto> DictionaryContatti()
        {
            return _dictio;
        }

        public Contatto GetByLastName(string lastName)
        {
            return(from contatto in _elenco
                   where contatto.Surname == lastName
                   select contatto).First();

            return (from contatto in _dictio.Values
                    where contatto.Surname == lastName
                    select contatto).First();
        }

        public Contatto GetFirstByName(string _name)
        {
            foreach (Contatto c in _elenco)
            {
                if (c.Name.Equals(_name))
                {
                    return c;
                }
            }
            //Avoid this: return null;
            throw new ArgumentOutOfRangeException("Nome non Trovato!");
        }

        public int Lenght { get { return _elenco.Count; } }
    }
    public abstract class Persona
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }

        public abstract string GetPersonType();
    }
    public class Contatto : Persona
    {
        protected static string[] NameList = { "Arnold", "Franco", "Giovanni",
                                  "Asia", "Giacomo"};
        protected static string[] SurnameList = { "Rossi", "Verdi",
                                    "\"Bomber\"",};
        protected static int _counter = 0;
       
        public string FullName 
        {
            get
            {
                return $"{this.Name} {this.Surname}";
            }
        }

        public override string GetPersonType()
        {
            return $"I'm a Contact";
        }
        public virtual void Contatta()
        {
            Console.WriteLine($"Sto chiamando {PhoneNumber} di {FullName} ");
        }

        public virtual void Contatta(string Messaggio)
        {
            Console.WriteLine($"Sto mandando {Messaggio} a {PhoneNumber} per {FullName}");
        }

        public Contatto(string name, string surname, string phoneNumber)
        {
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
        }
        
        public Contatto()
        {
            _counter++;
            //Console.WriteLine($"Costruttore di contatto n ({_counter})");
        }

        public override string ToString()
        {
            return $"{Name} {Surname}: {PhoneNumber}";
        }

        public static Contatto GetRandomContact()
        {
            Random r = new Random();
            int x = r.Next(NameList.Length);
            int y = r.Next(SurnameList.Length);
            return new Contatto()
            {
                Name = NameList[x],
                Surname = SurnameList[y],
                PhoneNumber = $"{x}-{y}",
            };
        }
    }

    public class EmailContatto : Contatto
    {
        public string Email { get; set; }   
        public EmailContatto()
        {
            Console.WriteLine("Nuovo EmailContatto");
        }

        public override string ToString()
        {
            return $"{base.ToString()} email: {this.Email}";
        }

    }
}
