using System;
using Library;

namespace StringExamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConvertToCamelCase();
            Console.ReadLine();
        }

        static void ConvertToCamelCase()
        {
            Console.Write("Please insert a string: ");
            string input = Console.ReadLine();

            string cleanCamelCase;
            string spacedCamelCase;

            StringLibrary.ToCamelCase(input, out cleanCamelCase, out spacedCamelCase);

            Console.WriteLine(cleanCamelCase);
            Console.WriteLine(spacedCamelCase);
        }
    }
}
