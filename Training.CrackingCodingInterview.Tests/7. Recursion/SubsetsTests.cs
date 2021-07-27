using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class SubsetsTests
    {
        List<Func<int[], IEnumerable<int[]>>> _actions = new List<Func<int[], IEnumerable<int[]>>>
        {
            Subsets.GetSubsets,
            Subsets.GetSubsetsYielding,
            Subsets.GetSubsetsCombinatorics
        };

        [Theory]
        [InlineData(
            new[] { 1, 2, 3 },
            new int[0],
            new[] { 1 },
            new[] { 1, 2 },
            new[] { 1, 2, 3 },
            new[] { 1, 3 },
            new[] { 2 },
            new[] { 2, 3 },
            new[] { 3 })]
        public void GetSubsetTest(int[] arr, params int[][] expected)
        {
            foreach (var action in _actions)
            {
                var sets = action.Invoke(arr);
                Common.AssertEqual(expected, sets.ToArray());
            }
        }
    }
}
