using System.Linq;

namespace Training.Codility.PrefixSums
{
    public static class PrefixSums
    {
        public static int[] CalculatePrefixSums(int[] a)
        {
            var n = a.Length;
            var p = new int[n + 1];
            foreach (var k in Enumerable.Range(1, n))
            {
                p[k] = p[k - 1] + a[k - 1];
            }
            return p;
        }

        public static int CountTotal(int[] prefixSums, int from, int to)
        {
            return prefixSums[to + 1] - prefixSums[from];
        }
    }
}