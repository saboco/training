using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class AnagramsTests
    {
        List<Func<string, IEnumerable<string>>> _actions = new List<Func<string, IEnumerable<string>>>
        {
            Anagrams.GetAnagramsIn,
            Anagrams.GetAnagramsBacktracking
        };
        [Theory]
        [InlineData("", new string[0])]
        [InlineData("a", new[] { "a" })]
        [InlineData("aaa", new[] { "aaa" })]
        [InlineData("ab", new[] { "ab", "ba" })]
        [InlineData("abc", new[] { "abc", "bac", "cba", "acb", "bca", "cab" })]
        [InlineData("aab", new[] { "aab", "baa", "aba" })]
        public void Should_return_all_anagrams_in_word(string s, string[] expected)
        {
            foreach (var action in _actions)
            {
                var anagrams = action.Invoke(s).ToArray();
                Assert.Equal(expected.Length, anagrams.Length);
                AssertAnagrams(expected, anagrams);
            }
        }

        private static void AssertAnagrams(IEnumerable<string> expected, string[] actual)
        {
            foreach (var expectedAnagram in expected)
            {
                Assert.Contains(expectedAnagram, actual);
            }
        }
    }
}