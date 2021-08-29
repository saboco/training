using System;
using System.Collections.Generic;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class LonguestSubsequenceTests
    {
        List<Func<int[], int>> _actions = new List<Func<int[], int>>
        {
            LonguestSubsequece.Longuest,
            LonguestSubsequece.LonguestIncreasingSubsequence,
            LonguestSubsequece.LonguestMemo,
            LonguestSubsequece.LonguestDp
        };

        [Theory]
        [InlineData(new[] { 5, 2, 3, 6, 8 }, 4)]
        [InlineData(new[] { 1, 5, 3, 2, 6, 8, 10 }, 5)]
        [InlineData(new[] { 9, 7, 1, 5, 3, 2, 6, 8, 10 }, 5)]
        public void LonguestSubsequenceTest(int[] arr, int expected)
        {
            foreach (var action in _actions)
            {
                var longuest = action.Invoke(arr);
                Assert.Equal(expected, longuest);
            }
        }
    }
}
