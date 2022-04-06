using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyConsoleFramework.ExtensionMethods;
using EasyConsoleFramework.Utils;

namespace EasyConsoleFramework.IO
{
    public class BaseIO
    {
        public static void PrintDictionary(Dictionary<string,string> dict)
        {
            StringBuilder sb = new StringBuilder();

            int maxKeyLenght = dict.Max(d => d.Key.ToString().Length);
            int maxValueLength = dict.Max(d => d.Value.ToString().Length);

            //dict.Select(d => sb.AppendLine(
            //    $"{d.Key.PadUntilLimit(10)} | {d.Value.PadUntilLimit(20)}"));

            foreach (string key in dict.Keys)
                sb.AppendLine($"{key.PadUntilLimit(10)}{dict[key].PadUntilLimit(20)}");

            Console.WriteLine(sb.ToString());
        }

        public static void PrintSortedDictionary(SortedDictionary<string, string> dict)
        {
            StringBuilder sb = new StringBuilder();

            int maxKeyLenght = dict.Max(d => d.Key.ToString().Length);
            int maxValueLength = dict.Max(d => d.Value.ToString().Length);

            //dict.Select(d => sb.AppendLine(
            //    $"{d.Key.PadUntilLimit(10)} | {d.Value.PadUntilLimit(20)}"));

            foreach (string key in dict.Keys)
                sb.AppendLine($"{key.PadUntilLimit(10)}{dict[key].PadUntilLimit(20)}");

            Console.WriteLine(sb.ToString());
        }

        public static string ReadFromConsole(string msg, Func<string, bool> condition)
        {
            string input;

            do
            {
                Console.Write(msg);
                input = Console.ReadLine();
            } while (!condition(input));

            return input;
        }
    }
}
