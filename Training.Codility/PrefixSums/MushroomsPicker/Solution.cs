using System;
using System.Linq;

namespace Training.Codility.PrefixSums.MushroomsPicker
{
    public class Solution
    {
        public static int Solve(int[] mushrooms, int m, int k)
        {
            var n = mushrooms.Length;
            var result = 0;
            var pref = PrefixSums.CalculatePrefixSums(mushrooms);
            foreach (var p in Enumerable.Range(0, Math.Min(m, k) + 1))
            {
                var leftPos = k - p;
                var rightPos = Math.Min(n - 1, Math.Max(k, k + m - 2 * p));
                result = Math.Max(result, PrefixSums.CountTotal(pref, leftPos, rightPos));
            }
            foreach (var p in Enumerable.Range(0, Math.Min(m + 1, n - k)))
            {
                var rightPos = k + p;
                var leftPos = Math.Max(0, Math.Min(k, k - (m - 2 * p)));
                result = Math.Max(result, PrefixSums.CountTotal(pref, leftPos, rightPos));
            }
            return result;
        }
    }
}