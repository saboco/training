using System;
using System.Collections.Generic;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class ReplaceStringTests
    {
        List<Func<string, string>> _actions = new List<Func<string, string>>
        {
            ReplaceSpaces.Replace,
            ReplaceSpaces.ScapeSpaces
        };

        [Theory]
        [InlineData("hello world", "hello%20world")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void ReplaceStringTest(string s, string expected)
        {
            foreach (var action in _actions)
            {
                var actual = action.Invoke(s);
                Assert.Equal(expected, actual);
            }
        }
    }
}
