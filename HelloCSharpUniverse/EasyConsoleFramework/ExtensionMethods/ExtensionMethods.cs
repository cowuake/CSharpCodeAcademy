using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyConsoleFramework.ExtensionMethods
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Pads a string (left and right) until the specified limit is reached
        /// </summary>
        /// <param name="str"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string PadUntilLimit(this string str, int limit)
        {
            if (str == null || limit < str.Length + 2)
                throw new ArgumentException();

            int availableSpace = limit - str.Length;

            string leftPadding = new String(' ', availableSpace / 2);
            string rightPadding = new String(' ', availableSpace - availableSpace / 2);

            return leftPadding + str + rightPadding;
        }

        /// <summary>
        /// Produces a human-readable representation for a generic dictionary
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static string ToFormattedString<K,V>(this IDictionary<K,V> dict)
        {
            StringBuilder sb = new StringBuilder();

            int maxKeyLenght = dict.Max(d => d.Key.ToString().Length) + 6;
            int maxValueLength = dict.Max(d => d.Value.ToString().Length) + 4;

            int lineLength = maxKeyLenght + maxValueLength + 2;
            string line = "\t" + new string('-', lineLength);

            sb.AppendLine(line);

            foreach (K key in dict.Keys)
                sb.AppendLine($"\t{$"[ {key.ToString()} ]".PadUntilLimit(maxKeyLenght)}" +
                    $" - {dict[key].ToString().PadUntilLimit(maxValueLength)}");

            sb.AppendLine(line);

            return sb.ToString();
        }


        public static string CenterInConsoleBuffer(this string str)
        {
            return str.PadUntilLimit(Console.BufferWidth);
        }

        public static int GetMaximumLineLength(this string str)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            return str.Split(Environment.NewLine).Max(l => l.Length);
        }
    }
}