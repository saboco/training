using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Common.Algorithms
{
    public class DijkstraShortestPath
    {
        public static int[] ShortestPath((int, int)[][] g, int start)
        {
            var q = new PriorityQueue<(int, int)>((parent, n) => n.Item2 < parent.Item2);
            var distances = new int[g.Length];
            var visited = new bool[g.Length];
            for (var i = 0; i < distances.Length; i++)
            {
                distances[i] = Int32.MaxValue;
            }
            distances[start] = 0;
            q.Enqueue((start, 0));

            while (q.Count > 0)
            {
                var (from, _) = q.Dequeue();
                visited[from] = true;
                foreach (var neighbor in g[from])
                {
                    var (to, weight) = neighbor;
                    if (visited[to])
                    { continue; }

                    var newDistance = distances[from] + weight;
                    if (newDistance < distances[to])
                    {
                        distances[to] = newDistance;
                        q.Enqueue((to, newDistance));
                    }
                }
            }
            return distances;
        }
    }
}
