using System;
using System.Collections.Generic;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class TreeCenterTests
    {
        List<Func<int[][], int[]>> _actions = new List<Func<int[][], int[]>>
        {
            TreeCenter.FindCenter,
            TreeCenter.FindCenterWithoutHelperStructure
        };

        [Theory]
        [InlineData(new[] { 1, 3 },
            new[] { 1 },
            new[] { 0, 3, 4 },
            new[] { 3 },
            new[] { 1, 2, 6, 7 },
            new[] { 1, 5, 8 },
            new[] { 4 },
            new[] { 3, 9 },
            new[] { 3 },
            new[] { 4 },
            new[] { 6 })]
        public void TreeCenterTest(int[] expectedCenters, params int[][] g)
        {
            foreach (var action in _actions)
            {
                var centers = action.Invoke(g);
                Common.AssertEqual(expectedCenters, centers);
            }
        }
    }
}
