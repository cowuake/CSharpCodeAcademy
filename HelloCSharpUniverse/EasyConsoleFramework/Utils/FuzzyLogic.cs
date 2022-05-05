using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyConsoleFramework.Utils
{
    public static class FuzzyLogic
    {
        public static int LevenshteinDistance(this string first, string second)
        {
            if (string.IsNullOrEmpty(first))
                return second.Length;

            if (string.IsNullOrEmpty(second))
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

        public static bool FindMostSimilarString(this string comparator, ICollection<string> candidates, out string chosen)
        {
            var distances = candidates.Select(c => c.LevenshteinDistance(comparator));

            var toBeChecked = candidates.Aggregate(
                (a, b) => a.LevenshteinDistance(comparator) < b.LevenshteinDistance(comparator) ? a : b);

            var bestCandidate = candidates.Zip(distances, (c, d) => (c, d))
                .Where((c, d) => d < Math.Min(c.ToString().Length, comparator.Length))
                .Select((c, d) => c)
                .First().c;

            if (bestCandidate == null)
            {
                chosen = null;
                return false;
            }

            chosen = bestCandidate;
            return true;
        }
    }
}