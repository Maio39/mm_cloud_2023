using System;

namespace Day10Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            DateTime start = DateTime.Now;
            List<Task> lista = new List<Task>();
            Console.WriteLine("start long");
            lista.Add(DoLong());
            for(int i = 0; i < 3; i++) 
            {
                Console.WriteLine("start short");
                lista.Add(DoShort());
            }
            Task.WaitAll(lista.ToArray());
            DateTime stop = DateTime.Now;
            Console.WriteLine($"Done in {(stop-start).TotalMilliseconds} ms");

            Console.ReadLine();
        }

        static async Task DoShort()
        {
            Console.WriteLine("Do Short");
            await Task.Delay(2000);
            Console.WriteLine($"End Short {DateTime.Now} {DateTime.Now.Millisecond}");
        }

        static async Task DoLong()
        {
            Console.WriteLine("Do Long");
            await Task.Delay(10000);
            Console.WriteLine($"End Long {DateTime.Now} {DateTime.Now.Millisecond}");
        }
    }
}