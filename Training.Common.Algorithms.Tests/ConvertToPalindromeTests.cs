using System;
using System.Collections.Generic;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class ConvertToPalindromeTests
    {
        List<Func<string, int>> _actions = new List<Func<string, int>>
        {
           //ConvertToPalindrome.MinDeletion,
            //ConvertToPalindrome.MinDeletionMemo,
            ConvertToPalindrome.MinDeletionDp
        };

        [Theory]
        [InlineData("KAZAYAKE", 3)]
        public void MinimumDeletionTest(string s, int expected)
        {
            foreach (var action in _actions)
            {
                var min = action.Invoke(s);
                Assert.Equal(expected, min);
            }
        }
    }
}
