using System;
using EasyConsoleFramework;

namespace EasyConsoleFramework.Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CLI cli = new CLI();

            cli.SetApplicationName("EASY CONSOLE FRAMEWORK DEMO");

            cli.AddAction("LEVEN", "Levenshtein distance DEMO", () => DemoCatalog.LevenshteinDistanceDemo());
            cli.AddAction("MOCK", "Print an example of mocked data in tabular form", () => DemoCatalog.PrintMockedData());
            cli.AddAction("SIMILAR", "Test similarity of a words to given set of sample elements", () => DemoCatalog.MostSimilarStringDemo());

            cli.Run();
        }
    }
}