using System;

namespace Avg
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Compute average ***");

            Console.Write("Insert first number: ");
            int firstNumber = int.Parse(Console.ReadLine());

            Console.Write("Insert second number: ");
            int secondNumber = int.Parse(Console.ReadLine());

            Console.WriteLine($"Average: {(firstNumber + secondNumber)/2.0f}");
        }
    }
}
