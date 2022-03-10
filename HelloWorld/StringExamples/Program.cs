using System;
using MainLibrary;

namespace StringExamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ConvertToCamelCase();
            StringLib.SplittingBenchmark();

            Console.Write("Press any button to exit...");
            Console.ReadKey();
            Environment.Exit(0);
        }

        static void ConvertToCamelCase()
        {
            Console.Write("Please insert a string: ");
            string input = Console.ReadLine();

            string cleanCamelCase;
            string spacedCamelCase;

            StringLib.ToCamelCase(input, out cleanCamelCase, out spacedCamelCase);

            Console.WriteLine(cleanCamelCase);
            Console.WriteLine(spacedCamelCase);
        }
    }
}
