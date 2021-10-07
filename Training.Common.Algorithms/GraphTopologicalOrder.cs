using System.Collections.Generic;

namespace Training.Common.Algorithms
{
    public class GraphTopologicalOrder
    {
        public static int[] TopologicalOrder(int[][] g)
        {
            var ordering = new List<int>();
            var visited = new bool[g.Length];
            for (var at = 0; at < g.Length; at++)
            {
                if (!visited[at])
                {
                    Dfs(g, at, visited, ordering);
                }
            }
            ordering.Reverse();
            return ordering.ToArray();
        }

        private static void Dfs(int[][] g, int at, bool[] visited, List<int> ordering)
        {
            visited[at] = true;
            foreach (var to in g[at])
            {
                if (!visited[to])
                {
                    Dfs(g, to, visited, ordering);
                }
            }
            ordering.Add(at);
        }

        public static int[] TopologicalOrderReverseInPlace(int[][] g)
        {
            var ordering = new int[g.Length];
            var visited = new bool[g.Length];
            var i = g.Length -1;
            for (var at = 0; at < g.Length; at++)
            {
                if (!visited[at])
                {
                    i=DfsReverseInPlace(g, i, at, visited, ordering);
                }
            }
            return ordering;
        }

        private static int DfsReverseInPlace(int[][] g, int i, int at, bool[] visited, int[] ordering)
        {
            visited[at] = true;
            foreach (var to in g[at])
            {
                if (!visited[to])
                {
                    i= DfsReverseInPlace(g, i, to, visited, ordering);
                }
            }
            ordering[i]=at;
            return i - 1;
        }
    }
}
