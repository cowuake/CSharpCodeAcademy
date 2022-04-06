using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyConsoleFramework.Utils;

namespace EasyConsoleFramework.IO
{
    public static class ToStringOverrides
    {
        public static string ToString(this Dictionary<string, string> dict)
        {
            StringBuilder sb = new StringBuilder();

            int maxKeyLenght = dict.Max(d => d.Key.Length);
            int maxValueLength = dict.Max(d => d.Value.Length);

            dict.Select(d => sb.AppendLine(
                $"{d.Key.PadUntilLimit(maxKeyLenght)} | {d.Value.PadUntilLimit(maxValueLength)}"));

            return sb.ToString();
        }
    }
}