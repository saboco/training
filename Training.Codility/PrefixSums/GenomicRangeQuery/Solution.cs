namespace Training.Codility.PrefixSums.GenomicRangeQuery
{
    public class Solution
    {
        public static int[] Solve(string s, int[] p, int[] q)
        {
            var result = new int[p.Length];

            var a = new int[s.Length + 1];
            var c = new int[s.Length + 1];
            var g = new int[s.Length + 1];

            for (var i = 0; i < s.Length; i++)
            {
                a[i + 1] = a[i] + (s[i] == 'A' ? 1 : 0);
                c[i + 1] = c[i] + (s[i] == 'C' ? 1 : 0);
                g[i + 1] = g[i] + (s[i] == 'G' ? 1 : 0);
            }

            for (var i = 0; i < p.Length; i++)
            {
                var pA = a[q[i] + 1] - a[p[i]];
                var pC = c[q[i] + 1] - c[p[i]];
                var pG = g[q[i] + 1] - g[p[i]];
                result[i] = pA > 0 ? 1 : pC > 0 ? 2 : pG > 0 ? 3 : 4;
            }
            return result;
        }
    }
}