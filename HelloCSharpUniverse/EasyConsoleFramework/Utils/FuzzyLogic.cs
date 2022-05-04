using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyConsoleFramework.Utils
{
    public static class FuzzyLogic
    {
        public static int LevenshteinDistance(string first, string second)
        {
            if (first.Length == 0)
                return second.Length;

            if (second.Length == 0)
                return first.Length;

            if (first[0] == second[0])
                return LevenshteinDistance(first.Substring(1), second.Substring(1));

            return 1 + new List<int>
            {
                LevenshteinDistance(first.Substring(1), second),
                LevenshteinDistance(first, second.Substring(1)),
                LevenshteinDistance(first.Substring(1), second.Substring(1))
            }.Min(n => n);
        }
    }
}