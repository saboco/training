using System;
using System.Collections.Generic;

namespace Training.Common.Algorithms
{
    public class DijkstraShortestPath
    {
        public static int[] ShortestPath((int, int)[][] g, int start, int end)
        {
            static int Compare((int, int c) a, (int, int c) b) => a.c < b.c ? -1 : a.c == b.c ? 0 : 1;
            var comparer = Comparer<(int, int)>.Create(Compare);
            var q = new PriorityQueue<(int, int)>(comparer);
            var distances = new int[g.Length];
            var visited = new bool[g.Length];
            Array.Fill(distances, Int32.MaxValue);

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

        public static int[] ShortestPathOptimization1((int, int)[][] g, int start, int end)
        {
            static int Compare((int, int c) a, (int, int c) b) => a.c < b.c ? -1 : a.c == b.c ? 0 : 1;
            var comparer = Comparer<(int, int)>.Create(Compare);
            var q = new PriorityQueue<(int, int)>(comparer);
            var distances = new int[g.Length];
            var visited = new bool[g.Length];
            Array.Fill(distances, Int32.MaxValue);

            distances[start] = 0;
            q.Enqueue((start, 0));

            while (q.Count > 0)
            {
                var (from, currentDistance) = q.Dequeue();
                visited[from] = true;

                if (distances[from] < currentDistance)
                { continue; }

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

        public static int[] ShortestPathOptimization2((int, int)[][] g, int start, int end)
        {
            static int Compare((int, int c) a, (int, int c) b) => a.c < b.c ? -1 : a.c == b.c ? 0 : 1;
            var comparer = Comparer<(int, int)>.Create(Compare);
            var q = new PriorityQueue<(int, int)>(comparer);
            var distances = new int[g.Length];
            var visited = new bool[g.Length];
            Array.Fill(distances, Int32.MaxValue);

            distances[start] = 0;
            q.Enqueue((start, 0));

            while (q.Count > 0)
            {
                var (from, currentDistance) = q.Dequeue();
                visited[from] = true;

                if (distances[from] < currentDistance)
                { continue; }

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
                if (from == end)
                {
                    break;
                }
            }
            return distances;
        }

        public static (int[], int[]) ShortestPathWithSteps((int, int)[][] g, int start, int end)
        {
            static int Compare((int, int c) a, (int, int c) b) => a.c < b.c ? -1 : a.c == b.c ? 0 : 1;
            var comparer = Comparer<(int, int)>.Create(Compare);
            var q = new PriorityQueue<(int, int)>(comparer);
            var visited = new bool[g.Length];
            var distances = new int[g.Length];
            var prev = new int[g.Length];
            Array.Fill(prev, -1);
            Array.Fill(distances, Int32.MaxValue);

            distances[start] = 0;
            q.Enqueue((start, 0));

            while (q.Count > 0)
            {
                var (from, currentDistance) = q.Dequeue();
                visited[from] = true;

                if (distances[from] < currentDistance)
                { continue; }

                foreach (var neighbor in g[from])
                {
                    var (to, weight) = neighbor;
                    if (visited[to])
                    { continue; }

                    var newDistance = distances[from] + weight;
                    if (newDistance < distances[to])
                    {
                        prev[to] = from;
                        distances[to] = newDistance;
                        q.Enqueue((to, newDistance));
                    }
                }
            }

            var path = GetPath(prev, distances, start, end);
            return (distances, path);
        }

        private static int[] GetPath(int[] prev, int[] distances, int start, int end)
        {
            if (distances[end] == Int32.MaxValue)
            { return Array.Empty<int>(); }

            var steps = new List<int>();
            var i = end;
            while (i != start)
            {
                steps.Add(i);
                i = prev[i];
            }
            steps.Add(start);
            steps.Reverse();
            return steps.ToArray();
        }
    }
}
