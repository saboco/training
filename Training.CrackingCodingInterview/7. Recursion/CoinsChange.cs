using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.CrackingCodingInterview
{
    public class CoinsChange
    {
        public static int CountWays(int n)
        {
            var ways = new HashSet<string>();
            CountWays(n, new List<char>(), ways);
            return ways.Count;
        }

        private static void CountWays(int n, List<char> current, HashSet<string> ways)
        {
            if (n < 0) { return; }
            if (n == 0)
            {
                var w = current.ToArray();
                Array.Sort(w);
                ways.Add(string.Join("", w));
                return;
            }

            current.Add('A');
            CountWays(n - 25, current, ways);
            current.RemoveAt(current.Count - 1);

            current.Add('B');
            CountWays(n - 10, current, ways);
            current.RemoveAt(current.Count - 1);

            current.Add('C');
            CountWays(n - 5, current, ways);
            current.RemoveAt(current.Count - 1);

            current.Add('D');
            CountWays(n - 1, current, ways);
            current.RemoveAt(current.Count - 1);
        }
    }
}
