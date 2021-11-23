using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Common.Algorithms
{
    public class EulerianPath
    {
        public static int[] FindEulerianPath(int[][] g)
        {
            var (inEdgesCount, outEdgesCount) = CountEdges(g);
            var totalEdges = outEdgesCount.Sum();

            if (!HasEulerianPath(inEdgesCount, outEdgesCount))
            { return Array.Empty<int>(); }
            var start = FindStartNode(inEdgesCount, outEdgesCount);
            var path = new List<int>();
            EulerianDfs(g, start, outEdgesCount, path);

            if (path.Count == totalEdges + 1)
            {
                path.Reverse();
                return path.ToArray();
            }
            return Array.Empty<int>();
        }

        private static void EulerianDfs(int[][] g, int at, int[] outEdgesCount, List<int> path)
        {
            while (outEdgesCount[at] != 0)
            {
                outEdgesCount[at]--;
                EulerianDfs(g, g[at][outEdgesCount[at]], outEdgesCount, path);
            }

            path.Add(at);
        }

        private static int FindStartNode(int[] inEdgesCount, int[] outEdgesCount)
        {
            var start = 0;
            for (var i = 0; i < inEdgesCount.Length; i++)
            {
                if (outEdgesCount[i] - inEdgesCount[i] == 1)
                {
                    return i;
                }

                if (outEdgesCount[i] > 0)
                {
                    start = i;
                }
            }
            return start;
        }

        private static bool HasEulerianPath(int[] inEdgesCount, int[] outEdgesCount)
        {
            var startNodes = 0;
            var endNodes = 0;
            for (var i = 0; i < inEdgesCount.Length; i++)
            {
                if (outEdgesCount[i] - inEdgesCount[i] > 1 || inEdgesCount[i] - outEdgesCount[i] > 1)
                {
                    return false;
                }
                else if (outEdgesCount[i] - inEdgesCount[i] == 1)
                {
                    startNodes++;
                }
                else if (inEdgesCount[i] - outEdgesCount[i] == 1)
                {
                    endNodes++;
                }
            }

            return (endNodes == 0 && startNodes == 0) || (endNodes == 1 && startNodes == 1);
        }

        private static (int[], int[]) CountEdges(int[][] g)
        {
            var inEdgesCount = new int[g.Length];
            var outEdgesCount = new int[g.Length];


            for (var at = 0; at < g.Length; at++)
            {
                outEdgesCount[at] = g[at].Length;
                foreach (var n in g[at])
                {
                    inEdgesCount[n]++;
                }
            }
            return (inEdgesCount, outEdgesCount);
        }
    }
}
