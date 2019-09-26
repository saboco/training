using System.Collections.Generic;
using Xunit;
using Training.HackerRank.Techniques_Concepts;

namespace Training.HackerRank.Tests.Techniques_Concepts
{
    public class DynamicProgrammingCoinChangeTests
    {
        [Theory]
        [InlineData(4, new[] { 1, 2, 3 }, 4)]
        [InlineData(10, new[] { 2, 5, 3, 6 }, 5)]
        [InlineData(100, new[] { 1, 5, 10, 25, 50 }, 292)]
        [InlineData(250, new[]
            {41, 34, 46, 9, 37, 32, 42, 21, 7, 13, 1, 24, 3, 43, 2, 23, 8, 45, 19, 30, 29, 18, 35, 11}, 15685693751)]
        public void Should_return_the_number_of_ways_to_make_change(int amount, int[] coins, long expected)
        {
            Assert.Equal(expected, DynamicProgrammingCoinChange.GetWaysOfMakeChange(amount, coins, new Dictionary<string, long>()));
        }
    }
}