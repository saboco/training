using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class SumTargetTests
    {
        List<Func<int[], int, IEnumerable<int[]>>> _actions = new List<Func<int[], int, IEnumerable<int[]>>>
        {
            SumTarget.GetSumsTarget,
            SumTarget.GetSumsTargetSkipDuplicates
        };

        [Theory]
        [InlineData(
            new[] { 1, 1, 6, 5, 2, 3, 4 },
            8,
            new[] { 1, 1, 2, 4 },
            new[] { 1, 1, 6 },
            new[] { 1, 2, 5 },
            new[] { 1, 3, 4 },
            new[] { 2, 6 },
            new[] { 3, 5 })]
        [InlineData(
            new[] { 1, 1, 6, 5, 2, 10, 7 },
            8,
            new[] { 1, 1, 6 },
            new[] { 1, 2, 5 },
            new[] { 1, 7 },
            new[] { 2, 6 })]
        public void SumTargetTest(int[] arr, int target, params int[][] expected)
        {
            foreach (var action in _actions)
            {
                var sums = action.Invoke(arr, target).ToArray();
                Common.AssertEqual(expected, sums);
            }
        }
    }
}
