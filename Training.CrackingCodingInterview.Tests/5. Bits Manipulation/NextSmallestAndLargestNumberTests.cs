using System;
using System.Collections.Generic;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class NextSmallestAndLargestNumberTests
    {
        [Theory]
        [InlineData("101010", "111", "11100000000000000000000000000000")]
        [InlineData("1", "1", "10000000000000000000000000000000")]
        [InlineData("0", "0", "0")]
        public void GetSmallestAndLargestNumberWithSame1BitsThatTest(string n, string expectedSmallest, string expectedLargest)
        {
            var i = Convert.ToInt32(n, 2);
            (var smallest, var largest) = NextSmallestAndLargestNumber.GetSmallestAndLargestNumberWithSame1BitsThat(i);
            var actualSmallest = Convert.ToString(smallest, 2);
            var actualLargest = Convert.ToString(largest, 2);
            Assert.Equal(expectedSmallest, actualSmallest);
            Assert.Equal(expectedLargest, actualLargest);
        }

        List<Func<int, (int, int)>> _actions = new List<Func<int, (int, int)>>
        {
            NextSmallestAndLargestNumber.GetNextSmallestAndNextLargestWithSame1BitsThatBrutForce,
            NextSmallestAndLargestNumber.GetNextSmallestAndNextLargestWithSame1BitsThat
        };
        [Theory]
        [InlineData("11111111111111111111111111111111", "11111111111111111111111111111111", "11111111111111111111111111111111")]
        [InlineData("11100", "11010", "100011")]
        [InlineData("1001", "110", "1010")]
        [InlineData("1", "1", "10")]
        [InlineData("0", "0", "0")]
        public void GetNextSmallestAndNextLargestWithSame1BitsThatTest(string n, string expectedSmallest, string expectedLargest)
        {
            var i = Convert.ToInt32(n, 2);
            foreach (var action in _actions)
            {
                (var smallest, var largest) = action.Invoke(i);
                var actualSmallest = Convert.ToString(smallest, 2);
                var actualLargest = Convert.ToString(largest, 2);
                Assert.Equal(expectedSmallest, actualSmallest);
                Assert.Equal(expectedLargest, actualLargest);
            }
        }
    }
}
