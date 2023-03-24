using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using static System.Formats.Asn1.AsnWriter;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace Day6Lab1
{
    public enum Status
    {
        None, Incorrect, Correct, CorrectButWrongPlace
    }
    public class ViewLingo
    {
        public string Key;
        public List<string> Tentative;
        public Status[] guessedLetters;
        public ViewLingo()
        {
            Key = Lingo.GenerateCasualWord();
            Tentative = new List<string>();
            guessedLetters = new Status[Key.Length];
        }

        public void clues(string tentative)
        {
            string correctLetters = "";
            for (int i = 0; i < tentative.Length; i++)
            {
                char current = tentative[i];
                if (current == Key[i])
                {
                    guessedLetters[i] = Status.Correct;
                    correctLetters += current;
                }
                if (Key.Contains(current) && current != Key[i] && !correctLetters.Contains(current))
                {
                    guessedLetters[i] = Status.CorrectButWrongPlace;
                }
                if (!Key.Contains(current) && current != Key[i])
                {
                    guessedLetters[i] = Status.Incorrect;
                }
            }
        }
        public void newGame()
        {
            Key = Lingo.GenerateCasualWord();
            Tentative = new List<string>();
            guessedLetters = new Status[Key.Length];

        }

    }
    public static class Lingo
    {
        static List<string> listOfWords = new List<string>();


        public static List<string> SetListOfWords()
        {
            List<string> list = new List<string>();
            using (StreamReader reader = new StreamReader("660000_parole_italiane.txt"))
            {
                string word = reader.ReadLine();
                while (word != null)
                {
                    list.Add(word);
                    word = reader.ReadLine();
                }
                reader.Close();
                return list;
            }
        }

        public static string GenerateCasualWord()
        {
            listOfWords = SetListOfWords();
            string random;
            do
            {
                Random rnd = new Random();
                int randIndex = rnd.Next(listOfWords.Count);
                random = listOfWords[randIndex];
            } while (random.Length > 5);
            return random;
        }

        public static string GetOscurateWord(string word,Status[] guessedLetters)
        {
            
                char[] lettersWord = word.ToCharArray();
                string oscurateWord = "";
                oscurateWord += lettersWord[0];
                for (int i = 1; i < word.Length; i++)
                {

                    if (guessedLetters[i] == Status.Correct)
                    {
                        oscurateWord += lettersWord[i];
                    }
                    else
                    {
                        oscurateWord += '_';
                    }
                }
                return oscurateWord;
            
        }
    }
        
            
}

