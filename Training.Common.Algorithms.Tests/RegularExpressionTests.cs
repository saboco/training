using System;
using System.Collections.Generic;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class RegularExpressionTests
    {
        List<Func<string, string, bool>> _actions = new List<Func<string, string, bool>>
        {
            RegularExpression.Match,
            RegularExpression.MatchMemo,
            RegularExpression.MatchDp,
        };

        [Theory]
        [InlineData("ABBBAC", ".*A*", true)]
        [InlineData("GREATS", "G*T*E", false)]
        [InlineData("", "", true)]
        [InlineData("", "*", true)]
        [InlineData("A", "", false)]
        [InlineData("A", "*", true)]
        [InlineData("AA", "*", true)]
        public void MatchTest(string s, string r, bool expected)
        {
            foreach (var action in _actions)
            {
                var match = action.Invoke(s, r);
                Assert.Equal(expected, match);
            }

        }
    }
}
