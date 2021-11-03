using System;
using System.Collections.Generic;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class DijkstraShortestPathTests
    {
        List<Func<(int, int)[][], int, int, int[]>> _actions = new List<Func<(int, int)[][], int, int, int[]>>
        {
            DijkstraShortestPath.ShortestPath,
            DijkstraShortestPath.ShortestPathOptimization1,
            DijkstraShortestPath.ShortestPathOptimization2
        };

        [Theory]
        [InlineData(
            new[] { 0, 3, 1, 4, 7 },
            5, 0, 4,
            new[] { 1, 2 },
            new[] { 3 },
            new[] { 1, 3 },
            new[] { 4 },
            new int[0],
            new[] { 4, 1 },
            new[] { 1 },
            new[] { 2, 5 },
            new[] { 3 },
            new int[0])]
        public void ShortesPathTest(int[] expected, int n, int start, int end, params int[][] g)
        {
            var graph = GetGraph(n, g);
            foreach (var action in _actions)
            {
                var shortestPath = action.Invoke(graph, start, end);
                Common.AssertEqual(expected, shortestPath);
            }
        }

        [Theory]
        [InlineData(
            new[] { 0, 3, 1, 4, 7 },
            new[] { 0, 2, 1, 3, 4 },
            5, 0, 4,
            new[] { 1, 2 },
            new[] { 3 },
            new[] { 1, 3 },
            new[] { 4 },
            new int[0],
            new[] { 4, 1 },
            new[] { 1 },
            new[] { 2, 5 },
            new[] { 3 },
            new int[0])]
        public void ShortesPathWithStepsTest(int[] expectedDistances, int[] expectedPath, int n, int start, int end, params int[][] g)
        {
            var graph = GetGraph(n, g);
            var (distances, path) = DijkstraShortestPath.ShortestPathWithSteps(graph, start, end);
            Common.AssertEqual(expectedDistances, distances);
            Common.AssertEqual(expectedPath, path);
        }

        private (int, int)[][] GetGraph(int n, int[][] g)
        {
            var nodes = new List<int[]>();
            var distances = new List<int[]>();
            var zipped = new List<(int, int)[]>();
            for (var i = 0; i < n; i++)
            {
                nodes.Add(g[i]);
            }
            for (var i = n; i < n * 2; i++)
            {
                distances.Add(g[i]);
            }

            for (var i = 0; i < n; i++)
            {
                var zip = new List<(int, int)>();
                for (var j = 0; j < nodes[i].Length; j++)
                {
                    zip.Add((nodes[i][j], distances[i][j]));
                }
                zipped.Add(zip.ToArray());
            }
            return zipped.ToArray();
        }
    }
}
