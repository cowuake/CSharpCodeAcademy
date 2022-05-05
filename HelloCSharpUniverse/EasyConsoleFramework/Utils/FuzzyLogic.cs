using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Math;

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
            IEnumerable<int> distances = candidates.Select(c => c.LevenshteinDistance(comparator));

            chosen = candidates
                .Zip(distances, (candidate, distance) => (candidate, distance))
                .Where(combo => combo.distance < Max(combo.candidate.Length, comparator.Length))
                .Aggregate((prev, next) => prev.distance < next.distance ? prev : next)
                .candidate;

            return chosen != null;
        }
    }
}