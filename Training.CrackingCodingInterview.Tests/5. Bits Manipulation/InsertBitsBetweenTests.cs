using System;
using System.Collections.Generic;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class InsertBitsBetweenTests
    {
        List<Func<int, int, int, int, int>> _actions = new List<Func<int, int, int, int, int>>()
        {
            InsertBetween.InsertBitsBetween,
            InsertBetween.InsertBitsBetween2
        };

        [Theory]
        [InlineData("10000000000", "10101", 2, 6, "10001010100")]
        [InlineData("10000000000", "10101", 3, 7, "10010101000")]
        [InlineData("10000000000", "101011", 2, 8, "10010101100")]
        public void InsertBitsBetweenTest(string s1, string s2, int i, int j, string expected)
        {
            var n = Convert.ToInt32(s1, 2);
            var m = Convert.ToInt32(s2, 2);
            foreach (var action in _actions)
            {
                var actual = Convert.ToString(action.Invoke(n, m, i, j), 2);
                Assert.Equal(expected, actual);
            }
        }
    }
}
