using System;

namespace Training.Common.Algorithms
{
    public class BellmanFord
    {
        public static double[] ShortestPath((int, int)[][] g, int start)
        {
            var V = g.Length;
            var distance = new double[V];
            Array.Fill(distance, double.PositiveInfinity);
            distance[start] = 0;

            for (var i = 0; i < V - 1; i++)
            {
                for (var from = 0; from < g.Length; from++)
                {
                    foreach (var (to, cost) in g[from])
                    {
                        if (distance[from] + cost < distance[to])
                        {
                            distance[to] = distance[from] + cost;
                        }
                    }
                }
            }

            for (var i = 0; i < V - 1; i++)
            {
                for (var from = 0; from < g.Length; from++)
                {
                    foreach (var (to, cost) in g[from])
                    {
                        if (distance[from] + cost < distance[to])
                        {
                            distance[to] = double.NegativeInfinity;
                        }
                    }
                }
            }


            return distance;
        }
    }
}
