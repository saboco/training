using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class BellmanFordTests
    {
        [Theory]
        [InlineData(
           new[] { 0d, 5d, double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, 35d, 40d, -10d, -20d, double.NegativeInfinity },
           10, 0,
           new[] { 1 },
           new[] { 2, 5, 6 },
           new[] { 3, 4 },
           new[] { 2 },
           new[] { 9 },
           new[] { 4, 6, 8 },
           new[] { 7 },
           new[] { 8 },
           new int[0],
           new int[0],
           new[] { 5 },
           new[] { 20, 30, 60 },
           new[] { 10, 75 },
           new[] { -15 },
           new[] { 100 },
           new[] { 25, 5, 50 },
           new[] { -50 },
           new[] { -10 },
           new int[0],
           new int[0])]
        public void ShortesPathTest(double[] expected, int n, int start, params int[][] g)
        {
            var graph = Common.GetAdjacencyList(n, g);

            var distances = BellmanFord.ShortestPath(graph, start);
            Common.AssertEqual(expected, distances);

        }
    }
}
