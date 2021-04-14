using Xunit;
using System;
using System.Collections.Generic;

namespace Training.CrackingCodingInterview.Tests
{
    public class AreTwoStringsAnagramsTests
    {
        List<Func<string, string, bool>> _actions = new List<Func<string, string, bool>>
        {
            AreTwoStringsAnagrams.AreAnagrams,
            AreTwoStringsAnagrams.AreAnagrams2,
            AreTwoStringsAnagrams.AreAnagrams3
        };
        [Theory]
        [InlineData("a", "a", true)]
        [InlineData("a", "aa", false)]
        [InlineData("aa", "aa", true)]
        [InlineData("anagram", "managra", true)]
        [InlineData("", "", true)]
        [InlineData(null, "", false)]
        [InlineData(null, null, false)]
        public void AreAnagramsTest(string s1, string s2, bool expected)
        {
            foreach (var action in _actions)
            {
                var actual = action.Invoke(s1, s2);
                Assert.Equal(expected, actual);
            }
        }
    }

}
