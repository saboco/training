using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class DijkstraShortestPathTests
    {
        [Theory]
        [InlineData(
            new[] { 0, 3, 1, 4, 7 },
            5, 0,
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
        public void ShortesPathTest(int[] expected, int n, int start, params int[][] g)
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

            var shortestPath = DijkstraShortestPath.ShortestPath(zipped.ToArray(), start);
            Common.AssertEqual(expected, shortestPath);
        }
    }
}
