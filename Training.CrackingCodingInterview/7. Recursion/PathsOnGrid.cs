using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.CrackingCodingInterview
{
    public class PathsOnGrid
    {
        public static int CountPaths(int n)
        {
            return CountPaths(n - 1, n - 1);
        }

        private static int CountPaths(int i, int j)
        {
            if (i == 0 && j == 0) { return 1; }
            if (i == 0) { return CountPaths(0, j - 1); }
            if (j == 0) { return CountPaths(i - 1, 0); }

            return CountPaths(i, j - 1) + CountPaths(i - 1, j);
        }

        public static int CountPaths(int[][] g)
        {
            var n = g.Length;
            return CountPaths(g, n - 1, n - 1);
        }

        private static int CountPaths(int[][] g, int i, int j)
        {
            if (i == 0 && j == 0 && g[0][0] == 0) { return 1; }
            if (i == 0 && j == 0 && g[0][0] == 1) { return 0; }
            if (i == 0 && g[0][j - 1] == 0) { return CountPaths(g, 0, j - 1); }
            if (j == 0 && g[i - 1][0] == 0) { return CountPaths(g, i - 1, 0); }

            var left = 0;
            if (g[i][j - 1] == 0)
            {
                left = CountPaths(g, i, j - 1);
            }
            var up = 0;
            if (g[i - 1][j] == 0)
            {
                up = CountPaths(g, i - 1, j);
            }

            return left + up;
        }
    }
}
