using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Common.Algorithms
{
    public class PrimsMinimumSpanningTree
    {
        public static (int, (int, int, int)[]) MinimumSpanningTree((int, int, int)[][] g)
        {
            var m = g.Length - 1;
            var edgeCount = 0;
            var visited = new bool[g.Length];
            var mstEdges = new (int, int, int)[m];
            var minCostSum = 0;

            static int Compare((int, int, int w) a, (int, int, int w) b) => a.w < b.w ? -1 : a.w == b.w ? 0 : 1;
            var comparer = Comparer<(int, int, int)>.Create(Compare);

            var pq = new PriorityQueue<(int from, int to, int weight)>(comparer);
            AddEdges(pq, g, visited, 0);

            while (pq.Count > 0 && edgeCount != m)
            {
                var edge = pq.Dequeue();
                if (visited[edge.to]) { continue; }

                mstEdges[edgeCount++] = edge;
                minCostSum += edge.weight;

                AddEdges(pq, g, visited, edge.to);
            }

            if (edgeCount < m)
            {
                return (-1, Array.Empty<(int, int, int)>());
            }

            return (minCostSum, mstEdges);
        }

        private static void AddEdges(PriorityQueue<(int, int, int)> pq, (int, int to, int)[][] g, bool[] visited, int nodeIndex)
        {
            visited[nodeIndex] = true;
            foreach (var edge in g[nodeIndex])
            {
                if (!visited[edge.to])
                {
                    pq.Enqueue(edge);
                }
            }
        }
    }
}
