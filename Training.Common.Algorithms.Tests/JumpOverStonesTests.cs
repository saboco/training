using System;
using System.Collections.Generic;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class JumpOverStonesTests
    {
        List<Func<int[], int, int>> _actions = new List<Func<int[], int, int>>
        { 
            //JumpOverStones.MinCost,
            //JumpOverStones.MinCostMemo,
            JumpOverStones.MinCostDp
        };

        [Theory]
        [InlineData(new[] { 20, 30, 40, 25, 15, 20, 28 }, 3, 73)]
        public void JumpOverStonesTest(int[] cost, int x, int expectedCost)
        {
            foreach (var action in _actions)
            {
                Assert.Equal(expectedCost, action.Invoke(cost, x));
            }
        }

        [Theory]
        [InlineData(new[] { 20, 30, 40, 25, 15, 20, 28 }, 3, 73, new[] { 0, 2, 5, 7 })]
        public void MinCostWithPathTest(int[] cost, int x, int expectedCost, int[] expectedPath)
        {
            var (minCost, path) = JumpOverStones.MinCostDpWithPath(cost, x);
            Assert.Equal(expectedCost, minCost);
            Common.AssertEqual(expectedPath, path);
        }
    }
}
