using System;
using System.Collections.Generic;
using System.Linq;

namespace Training.Common.Algorithms
{
    public class ShortestLonguestPathsOnDags
    {
        public static int ShortestPath((int, int)[][] g, int start)
        {
            var onlyNodes = g.Select(e => e.Select(n => n.Item1).ToArray()).ToArray();
            var ordering = GraphTopologicalOrder.TopologicalOrderReverseInPlace(onlyNodes);
            var path = new int[ordering.Length];
            Array.Fill(path, Int32.MaxValue);
            path[start] = 0;
            for (var i = 0; i < g.Length; i++)
            {
                var from = ordering[i];
                if (path[from] < Int32.MaxValue)
                {
                    var neighbors = g[from];
                    for (var j = 0; j < neighbors.Length; j++)
                    {
                        var (to, distance) = neighbors[j];
                        var newDistance = path[from] + distance;
                        if (newDistance < path[to])
                        {
                            path[to] = newDistance;
                        }
                    }
                }
            }
            return path[g.Length - 1];
        }

        public static int LongestPath((int, int)[][] g, int start)
        {
            var onlyNodes = g.Select(e => e.Select(n => n.Item1).ToArray()).ToArray();
            var ordering = GraphTopologicalOrder.TopologicalOrderReverseInPlace(onlyNodes);
            var path = new int[ordering.Length];
            Array.Fill(path, Int32.MaxValue);
            path[start] = 0;
            for (var i = 0; i < g.Length; i++)
            {
                var from = ordering[i];
                if (path[from] < Int32.MaxValue)
                {
                    var neighbors = g[from];
                    for (var j = 0; j < neighbors.Length; j++)
                    {
                        var (to, distance) = neighbors[j];
                        var newDistance = path[from] + (distance * -1);
                        if (newDistance < path[to])
                        {
                            path[to] = newDistance;
                        }
                    }
                }
            }
            return path[g.Length - 1] * -1;
        }
    }
}