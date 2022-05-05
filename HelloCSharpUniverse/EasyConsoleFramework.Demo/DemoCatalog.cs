using EasyConsoleFramework.Demo.Models;
using EasyConsoleFramework.Extensions;
using EasyConsoleFramework.IO;
using EasyConsoleFramework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyConsoleFramework.Demo
{
    internal static class DemoCatalog
    {
        internal static void PrintMockedData(int number = 25)
        {
            IList<StupidEntity> stupidEntities = new List<StupidEntity>();

            foreach (int _ in Enumerable.Range(1, number))
                stupidEntities.Add(new StupidEntity());

            IList<int> columnWidths = new List<int>() { 20, 25, 25, 20, 20 };

            Console.WriteLine(stupidEntities.ToFormattedString(columnWidths, alignment: "left"));
            Console.WriteLine(stupidEntities.ToFormattedString(columnWidths, alignment: "right"));
            Console.WriteLine(stupidEntities.ToFormattedString(columnWidths, alignment: "center"));
        }

        internal static void LevenshteinDistanceDemo()
        {
            string first = BaseIO.ReadFromConsole("\tFirst word: ");
            string second = BaseIO.ReadFromConsole("\tSecond word: ");
            Console.WriteLine($"\tLevenshtein distance: {first.LevenshteinDistance(second)}");
            Console.WriteLine();
        }

        internal static void MostSimilarStringDemo()
        {
            string[] words = new string[] { "cat", "dog", "Boba Fett", "trebuchet" };

            Console.WriteLine($"\tSample words are: {string.Join(", ", words)}");
            string comparer = BaseIO.ReadFromConsole("\tWord to be compared: ");

            bool foundCandidate = comparer.FindMostSimilarString(words, out string candidate);

            if (foundCandidate)
            {
                Console.WriteLine("\tNo word in the sample is a good enough candidate.");
                Console.WriteLine();
                return;
            }

            Console.WriteLine($"\tThe best candidate is: {candidate}");
            Console.WriteLine();
        }
    }
}