using System;
using System.Collections.Generic;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class RodCuttingTests
    {
        List<Func<int, int[], (int, int[])>> _actions = new List<Func<int, int[], (int, int[])>>
        {
            RodCutting.CutRod,
            //RodCutting.CutRodMemo,
            //RodCutting.CutRodDp
        };
        [Theory]
        [InlineData(8, new[] { 1, 5, 8, 9, 10, 14, 17, 20, 24, 30 }, 21, new[] { 2, 3, 3 })]
        [InlineData(6, new[] { 1, 5, 8, 9, 10, 14, 17, 20, 24, 30 }, 16, new[] { 3, 3 })]
        [InlineData(1, new[] { 1, 5, 8, 9, 10, 14, 17, 20, 24, 30 }, 1, new []{ 1 })]
        [InlineData(0, new[] { 1, 5, 8, 9, 10, 14, 17, 20, 24, 30 }, 0, new int[0])]
        [InlineData(2, new[] { 1, 5, 8, 9, 10, 14, 17, 20, 24, 30 }, 5, new int[] { 2 })]
        public void RodCuttingTest(int rod, int[] prices, int expected, int[] expectedBestCut)
        {
            foreach (var action in _actions)
            {
                var (profit, bestCut) = action.Invoke(rod, prices);
                Assert.Equal(expected, profit);
                Common.AssertEqual(expectedBestCut, bestCut);
            }
        }
    }
}
