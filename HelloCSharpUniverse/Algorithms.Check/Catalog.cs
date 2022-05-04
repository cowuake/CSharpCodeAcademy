using EasyConsoleFramework.IO;
using Algorithms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Check
{
    internal static class Catalog
    {
        internal static void Factorial()
        {
            int number = ReadInteger().Value;

            Console.WriteLine($"\tThe result is: {Recursion.Classics.Factorial(number)}");
            Console.WriteLine();
        }

        internal static void Fibonacci()
        {
            int number = ReadInteger().Value;

            Console.WriteLine($"\tThe result is: {Recursion.Classics.Fibonacci(number)}");
            Console.WriteLine();
        }

        private static int? ReadInteger()
        {
            int number = int.MinValue;

            BaseIO.ReadFromConsole(
                "\tPlease insert an integer: ",
                s => int.TryParse(s, out number));

            if (number == int.MinValue)
                return null;

            return number;
        }
    }
}