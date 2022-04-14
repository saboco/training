using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class PermutationsTests
    {
        readonly List<Func<int[], IEnumerable<int[]>>> _actions = new()
        {
            Permutations.GetPermutations
        };

        [Theory]
        [InlineData(
            new[] { 1, 2, 3 },
            new[] { 1, 2, 3 },
            new[] { 1, 3, 2 },
            new[] { 2, 1, 3 },
            new[] { 2, 3, 1 },
            new[] { 3, 1, 2 },
            new[] { 3, 2, 1 })]
        public void PermutationsTest(int[] arr, params int[][] expected)
        {
            foreach (var action in _actions)
            {
                var permutations = action.Invoke(arr);
                Common.AssertEqual(expected, permutations.ToArray());
            }
        }

    }
}
