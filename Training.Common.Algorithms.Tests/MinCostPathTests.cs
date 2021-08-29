using System;
using System.Collections.Generic;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class MinCostPathTests
    {
        List<Func<int[,], int>> _actions = new List<Func<int[,], int>>
        {
            MinCostPath.MinCost,
            MinCostPath.MinCostMemo,
            MinCostPath.MinCostDp
        };

        [Theory]
        [InlineData(192,
            new[] { 5, 47, 8, 18, 1 },
            new[] { 43, 25, 39, 36, 13 },
            new[] { 22, 8, 13, 38, 46 },
            new[] { 41, 41, 40, 25, 44 },
            new[] { 29, 43, 22, 50, 10 })]
        public void MinCostPathTest(int expectedCost, params int[][] costs)
        {
            var c = new int[costs.Length, costs[0].Length];
            for (var i = 0; i < c.GetLength(0); i++)
            {
                for (var j = 0; j < c.GetLength(1); j++)
                {
                    c[i, j] = costs[i][j];
                }
            }
            foreach (var action in _actions)
            {
                var cost = action.Invoke(c);
                Assert.Equal(expectedCost, cost);
            }
        }


        [Theory]
        [InlineData(192,
            "(0,0)->(0,1)->(0,2)->(0,3)->(0,4)->(1,4)->(2,4)->(3,4)->(4,4)",
            new[] { 5, 47, 8, 18, 1 },
            new[] { 43, 25, 39, 36, 13 },
            new[] { 22, 8, 13, 38, 46 },
            new[] { 41, 41, 40, 25, 44 },
            new[] { 29, 43, 22, 50, 10 })]
        public void MinCostPathWithStepsTest(int expectedCost, string expectedPath, params int[][] costs)
        {
            var c = new int[costs.Length, costs[0].Length];
            for (var i = 0; i < c.GetLength(0); i++)
            {
                for (var j = 0; j < c.GetLength(1); j++)
                {
                    c[i, j] = costs[i][j];
                }
            }

            var (cost, path) = MinCostPath.MinCostDpPath(c);
            Assert.Equal(expectedCost, cost);
            Assert.Equal(expectedPath, path);
        }
    }
}
