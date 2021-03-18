using System;
using Xunit;

namespace Training.Leetcode.Tests
{
    public class RotateStringTests
    {
        private readonly Func<string, string, bool>[] _actions =
        {
            RotateString.IsRotateString,
            RotateString.IsRotateStringKMP
        };

        [Theory]
        [InlineData(true, "abcdefg", "gabcdef")]
        public void RotateStringTest(bool expected, string a, string b)
        {
            foreach (var action in _actions)
            {
                Assert.Equal(expected, action.Invoke(a, b));
            }
        }
    }
}
