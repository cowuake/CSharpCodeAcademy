using EasyConsoleFramework.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                sb.AppendLine($"\t{$"[ {key.ToString()} ]".PadUntilLimit(maxKeyLenght).ToBold()}" +
                    $" - {dict[key].ToString().PadUntilLimit(maxValueLength).ToItalic()}");

            sb.AppendLine(line);

            return sb.ToString();
        }

        /// <summary>
        /// Pad a string so that it results to be approximately centered in Console buffer
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CenterInConsoleBuffer(this string str)
        {
            return str.PadUntilLimit(Console.BufferWidth);
        }

        /// <summary>
        /// Get maximum line length for lines in a string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static int GetMaxLineLength(this string str)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            return str.Split(Environment.NewLine.ToCharArray()).Max(l => l.Length);
        }

        public static string Truncate(this string str, int maxLength, bool ellipsis = true)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            if (maxLength <= 0)
                return str;

            if (ellipsis)
                return str.Substring(0, maxLength - 3) + "...";

            return str.Substring(0, maxLength);
        }

        /// <summary>
        /// Formats an enumerable object containing public objects in tabular form
        /// </summary>
        /// <param name="enumerable">The Enumerable to be formatted</param>
        /// <param name="columnTitles"></param>
        /// <param name="columnWidths">The widhts to be used for each column</param>
        /// <param name="alignment">The desired text alignment (left/right/center)</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string ToFormattedString
            (
            this IEnumerable<object> enumerable,
            IList<int> columnWidths = null,
            IList<string> columnTitles = null,
            string alignment = "left"
            )
        {
            if (columnWidths != null && columnWidths.Count != enumerable.Count())
                throw new ArgumentException(nameof(columnTitles));

            if (columnTitles != null && columnTitles.Count != enumerable.Count())
                throw new ArgumentException(nameof(columnTitles));

            if (!new string[] { "left", "right", "center" }.Contains(alignment))
                throw new ArgumentException(nameof(alignment));

            Type type = enumerable.First().GetType();

            var properties = type.GetProperties();

            var propertyNames = properties
                .Select(p => p.Name)
                .ToList();

            var propertyTypes = properties
                .Select(p => p.GetType())
                .ToList();

            if (columnTitles == null)
                columnTitles = propertyNames;

            int lineLength = columnWidths.Sum() + columnTitles.Count;

            string ruleLine = new string('-', lineLength);

            string header = "";

            for (int i = 0; i < columnTitles.Count; i++)
            {
                if (alignment == "left")
                    header += $"{columnTitles[i].PadLeft(columnWidths[i])}";
                else if (alignment == "right")
                    header += $"{columnTitles[i].PadRight(columnWidths[i])}";
                else if (alignment == "center")
                    header += $"{columnTitles[i].PadUntilLimit(columnWidths[i])}";
            }
            

            var sb = new StringBuilder();

            sb.AppendLine(ruleLine);
            sb.AppendLine(header);
            sb.AppendLine(ruleLine);

            foreach (object item in enumerable)
            {
                string row = "";

                for (int i = 0; i < properties.Count(); i++)
                {
                    string value = item
                        .GetType()
                        .GetProperty(propertyNames[i])
                        .GetValue(item, null)
                        .ToString();

                    if (alignment == "left")
                        row += value.PadLeft(columnWidths[i]);
                    else if (alignment == "right")
                        row += value.PadRight(columnWidths[i]);
                    else if (alignment == "center")
                        row += value.PadUntilLimit(columnWidths[i]);
                }

                sb.AppendLine(row);
            }

            sb.AppendLine(ruleLine);

            return sb.ToString();
        }

        /// <summary>
        /// Returns string in bold
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToBold(this string str)
            => $"{ANSI_ESCAPE_CODE.BOLD}{str}{ANSI_ESCAPE_CODE.NOT_BOLD}";

        /// <summary>
        /// Returns string in italic
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToItalic(this string str)
            => $"{ANSI_ESCAPE_CODE.ITALIC}{str}{ANSI_ESCAPE_CODE.NOT_ITALIC}";

        /// <summary>
        /// Returns string underlined
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToUnderlined(this string str)
            => $"{ANSI_ESCAPE_CODE.UNDERLINED}{str}{ANSI_ESCAPE_CODE.NOT_UNDERLINED}";

        internal static int NDigits(this int number)
            => number.ToString().Length;

        internal static decimal ToFractional(this int number)
            => number / 10 ^ (number.NDigits() - 1);
    }
}