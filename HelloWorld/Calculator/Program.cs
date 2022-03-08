using System;
using Library;

namespace Calculator
{
    internal class Program
    {

        public bool IsNatural(int number)
        {
            return number >= 0;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("*** CALCULATOR ***");

            int n = InputLib.ReadIntegerFromConsole("", x => x >= 0);

            int[] array = new int[n];
            Random rnd = new Random();

            for (int i = 0; i < n; i++)
            {
                array[i] = rnd.Next(-100, 101);
            }

            do
            {
                PrintMenu();
                Console.WriteLine("Choose an option");

                string choice = Console.ReadLine();

                //switch (choice)
                //{
                //    case "P":
                //        IntArrayLib.Print(array);
                //    case "Q":
                //}
            } while (true);
        }

        static void PrintMenu()
        {
            Console.WriteLine("****************");
            Console.WriteLine("S - somma");
            Console.WriteLine("M - max");
            Console.WriteLine("m - min");
            Console.WriteLine("P - print");
            Console.WriteLine("O - riordina");
            Console.WriteLine("A - media");
            Console.WriteLine("H - help");
            Console.WriteLine("Q - exit");
        }
    }
}
