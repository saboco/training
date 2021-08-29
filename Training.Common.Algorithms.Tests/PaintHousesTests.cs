using System;
using System.Collections.Generic;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class PaintHousesTests
    {
        List<Func<int[][], (int, int[])>> _actions = new List<Func<int[][], (int, int[])>>
        {
            PaintHouses.MinCost,
            PaintHouses.MinCostMemo,
            PaintHouses.MinCostDp,
        };

        [Theory]
        [InlineData(10, new[] { 1, 2, 1 }, new[] { 17, 2, 17 }, new[] { 16, 15, 5 }, new[] { 14, 3, 9 })]
        [InlineData(61,
            new[] { 1, 2, 0, 1, 0, 1, 0, 2, 1, 2 },
            new[] { 17, 2, 17 },
            new[] { 16, 15, 5 },
            new[] { 2, 12, 8 },
            new[] { 14, 3, 6 },
            new[] { 15, 9, 24 },
            new[] { 14, 7, 26 },
            new[] { 10, 20, 16 },
            new[] { 12, 7, 6 },
            new[] { 14, 4, 10 },
            new[] { 11, 3, 7 })]
        public void PaintHousesTest(int expectedCost, int[] expectedColors, params int[][] costs)
        {
            foreach (var action in _actions)
            {
                var (cost, colors) = action.Invoke(costs);
                Assert.Equal(expectedCost, cost);
                Common.AssertEqual(expectedColors, colors);
            }
        }
    }
}
