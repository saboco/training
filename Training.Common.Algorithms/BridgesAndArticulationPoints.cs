using System;
using System.Collections.Generic;

namespace Training.Common.Algorithms
{
    public class BridgesAndArticulationPoints
    {
        public static (int, int)[] FindBridges(int[][] g)
        {
            var visited = new bool[g.Length];
            var ids = new int[g.Length];
            var lowLink = new int[g.Length];
            var id = 0;

            var bridges = new List<(int, int)>();

            for (var i = 0; i < g.Length; i++)
            {
                if (!visited[i])
                {
                    DfsBridges(g, i, -1, id, ids, visited, lowLink, bridges);
                }
            }

            return bridges.ToArray();
        }

        private static void DfsBridges(int[][] g, int at, int parent, int id, int[] ids, bool[] visited, int[] lowLink, List<(int, int)> bridges)
        {
            visited[at] = true;
            lowLink[at] = id;
            ids[at] = id;
            foreach (var to in g[at])
            {
                if (to == parent)
                { continue; }

                if (!visited[to])
                {
                    DfsBridges(g, to, at, id + 1, ids, visited, lowLink, bridges);
                    lowLink[at] = Math.Min(lowLink[at], lowLink[to]);
                    if (ids[at] < lowLink[to])
                    {
                        bridges.Add((at, to));
                    }
                }
                else
                {
                    lowLink[at] = Math.Min(lowLink[at], ids[to]);
                }
            }
        }

        public static bool[] FindArticulationPoints(int[][] g)
        {
            var id = 0;
            var visited = new bool[g.Length];
            var ids = new int[g.Length];
            var lowLink = new int[g.Length];
            var isArticulation = new bool[g.Length];

            for (var i = 0; i < g.Length; i++)
            {
                if (!visited[i])
                {
                    var outEdgeCount = DfsArticulationPoints(g, i, i, -1, id, ids, 0, lowLink, isArticulation, visited);
                    isArticulation[i] = outEdgeCount > 1;
                }
            }

            return isArticulation;
        }

        private static int DfsArticulationPoints(int[][] g, int root, int at, int parent, int id, int[] ids, int outEdgeCount, int[] lowLink, bool[] isArticulation, bool[] visited)
        {
            if(parent==root)
            { outEdgeCount++; }

            visited[at] = true;
            lowLink[at] = at;
            ids[at] = at;
            foreach (var to in g[at])
            {
                if (parent == to)
                { continue; }

                if (!visited[to])
                {
                    DfsArticulationPoints(g, root, to, at, id + 1, ids, outEdgeCount, lowLink, isArticulation, visited);
                    lowLink[at] = Math.Min(lowLink[at], lowLink[to]);
                    if (ids[at] <= lowLink[to])
                    {
                        isArticulation[at] = true;
                    }
                }
                else
                {
                    lowLink[at] = Math.Min(lowLink[at], ids[to]);
                }
            }

            return outEdgeCount;
        }
    }
}
