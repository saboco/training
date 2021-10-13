using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class ShortestLonguestPathsOnDagsTests
    {
        [Theory]
        [InlineData(11, 8, 0,
            new[] { 1, 2 },
            new[] { 2, 3, 4 },
            new[] { 3, 6 },
            new[] { 4, 5, 6 },
            new[] { 7 },
            new[] { 7 },
            new[] { 7 },
            new int[0],
            new[] { 3, 6 },
            new[] { 4, 4, 11 },
            new[] { 8, 11 },
            new[] { -4, 5, 2 },
            new[] { 9 },
            new[] { 1 },
            new[] { 2 },
            new int[0])]
        [InlineData(4, 8, 3,
            new[] { 1, 2 },
            new[] { 2, 3, 4 },
            new[] { 3, 6 },
            new[] { 4, 5, 6 },
            new[] { 7 },
            new[] { 7 },
            new[] { 7 },
            new int[0],
            new[] { 3, 6 },
            new[] { 4, 4, 11 },
            new[] { 8, 11 },
            new[] { -4, 5, 2 },
            new[] { 9 },
            new[] { 1 },
            new[] { 2 },
            new int[0])]
        public void ShortesPathTest(int expected, int n, int start, params int[][] g)
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

            var shortestPath = ShortestLonguestPathsOnDags.ShortestPath(zipped.ToArray(), start);
            Assert.Equal(expected, shortestPath);
        }

        [Theory]
        [InlineData(23, 8, 0,
            new[] { 1, 2 },
            new[] { 2, 3, 4 },
            new[] { 3, 6 },
            new[] { 4, 5, 6 },
            new[] { 7 },
            new[] { 7 },
            new[] { 7 },
            new int[0],
            new[] { 3, 6 },
            new[] { 4, 4, 11 },
            new[] { 8, 11 },
            new[] { -4, 5, 2 },
            new[] { 9 },
            new[] { 1 },
            new[] { 2 },
            new int[0])]
        [InlineData(6, 8, 3,
            new[] { 1, 2 },
            new[] { 2, 3, 4 },
            new[] { 3, 6 },
            new[] { 4, 5, 6 },
            new[] { 7 },
            new[] { 7 },
            new[] { 7 },
            new int[0],
            new[] { 3, 6 },
            new[] { 4, 4, 11 },
            new[] { 8, 11 },
            new[] { -4, 5, 2 },
            new[] { 9 },
            new[] { 1 },
            new[] { 2 },
            new int[0])]
        public void LongestPathTest(int expected, int n, int start, params int[][] g)
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

            var shortestPath = ShortestLonguestPathsOnDags.LongestPath(zipped.ToArray(), start);
            Assert.Equal(expected, shortestPath);
        }
    }
}
