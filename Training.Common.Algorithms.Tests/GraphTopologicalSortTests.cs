using System;
using System.Collections.Generic;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class GraphTopologicalSortTests
    {
        List<Func<int[][], int[]>> _actions = new List<Func<int[][], int[]>>
        {
            GraphTopologicalOrder.TopologicalOrder,
            GraphTopologicalOrder.TopologicalOrderReverseInPlace
        };
        // A B C D E F G H I J
        // 0 1 2 3 4 5 6 7 8 9 
        [Theory]
        [InlineData(new[] { 2, 0, 3, 5, 1, 4, 7, 6, 9, 8 },
            new[] { 1, 3 },
            new[] { 4, },
            new[] { 5 },
            new[] { 4, 5 },
            new[] { 6, 7 },
            new[] { 6, 8 },
            new[] { 8, 9 },
            new[] { 9 },
            new int[0],
            new int[0])]
        public void TopologicalSortTest(int[] expected, params int[][] g)
        {
            foreach (var action in _actions)
            {
                var orderings = action.Invoke(g);
                Common.AssertEqual(expected, orderings);
            }
        }
    }
}
