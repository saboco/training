using System;
using System.Collections.Generic;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class KnapsackTests
    {
        List<Func<int[], int[], int, (int, int[])>> _actions = new List<Func<int[], int[], int, (int, int[])>>
        {
             Knapsack.BestKnapsack,
             Knapsack.BestKnapsackMemo,
             Knapsack.BestKnapsackDp
        };

        [Theory]
        [InlineData(new[] { 3, 7, 10, 6 }, new[] { 4, 14, 10, 5 }, 20, 28, new[] { 0, 1, 2 })]
        public void KanpsackTest(int[] weights, int[] values, int w, int expectedValue, int[] expectedKanpsack)
        {
            foreach (var action in _actions)
            {
                var (best, knapsack) = action.Invoke(weights, values, w);
                Assert.Equal(expectedValue, best);
                Array.Sort(knapsack);
                Common.AssertEqual(expectedKanpsack, knapsack);
            }
        }
    }
}
