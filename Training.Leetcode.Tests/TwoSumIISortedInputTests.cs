using System;
using Xunit;

namespace Training.Leetcode.Tests
{
    public class TwoSumIISortedInputTests
    {
        private readonly Func<int[], int, int[]>[] _actions =
        {
            TwoSumIISortedInput.TwoSum,
            TwoSumIISortedInput.TwoSum2
        };

        [Theory]
        [InlineData(new[] { 1, 2 }, 9, new[] { 2, 7, 11, 15 })]
        [InlineData(new[] { 3, 5 }, 0, new[] { -5, -2, -1, 0, 1, 3, 4, 9 })]
        [InlineData(new[] { 4, 5 }, 4, new[] { -1, -2, 0, 2, 2, 7, 11, 15 })]
        public void TwoIISortedInputTest(int[] expected, int target, int[] nums)
        {
            foreach (var action in _actions)
            {
                var actual = action.Invoke(nums, target);
                for (var i = 0; i < expected.Length; i++)
                {
                    Assert.Equal(expected[i], actual[i]);
                }
            }
        }
    }
}
