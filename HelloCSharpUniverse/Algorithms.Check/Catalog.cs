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
            int number = -1;

            BaseIO.ReadFromConsole(
                "\tPlease insert an integer: ",
                s => int.TryParse(s, out number));

            Console.WriteLine(Recursion.Classics.Factorial(number));
            Console.WriteLine();
        }
    }
}