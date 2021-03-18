using System;
using Xunit;

namespace Training.Leetcode.Tests
{
    public class ImplementStrStrTests
    {
        [Theory]
        [InlineData(0, "hello", null)]
        [InlineData(0, "hello", "")]
        [InlineData(-1, "", "a")]
        [InlineData(-1, null, "a")]
        [InlineData(0, "", "")]
        [InlineData(2, "hello", "ll")]
        [InlineData(10, "ababcabcabababd", "ababd")]
        [InlineData(-1, "aaaa", "bba")]
        [InlineData(-1, "aaaa", "ab")]
        [InlineData(0, "a", "a")]
        [InlineData(-1, "aaa", "aaaa")]
        [InlineData(0, "mississippi", "mississippi")]
        [InlineData(4, "mississippi", "issip")]
        [InlineData(4, "aabaaabaaac", "aabaaac")]
        public void ImplementStrStrTest(int expected, string haystack, string needle)
        {
            Assert.Equal(expected, ImplementStrStr.StrStr(haystack, needle));
            Assert.Equal(expected, ImplementStrStr.StrStr2(haystack, needle));
            Assert.Equal(expected, ImplementStrStr.KMP(haystack, needle));
        }

        private readonly Func<string, int[]>[] _lpsActions =
        {
            ImplementStrStr.LpsTable            
        };

        [Theory]
        [InlineData(new[] { 0, 0, 0, 0, 1, 2, 0, 1, 2, 0 }, "abcdabeabf")]
        [InlineData(new[] { 0, 0, 0, 0, 0, 1, 2, 0, 1, 2, 3 }, "abcdeabfabc")]
        [InlineData(new[] { 0, 1, 0, 0, 1, 0, 1, 2, 3, 0 }, "aabcadaabe")]
        [InlineData(new[] { 0, 1, 2, 3, 0, 1, 2, 0, 0 }, "aaaabaacd")]
        [InlineData(new[] { 0, 1, 0, 1, 2, 2, 0 }, "aabaaac")]
        public void CorrectGeneratingLPS(int[] expected, string pattern)
        {
            foreach (var action in _lpsActions)
            {
                var actual = action.Invoke(pattern);
                Assert.Equal(expected.Length, actual.Length);
                for (var i = 0; i < expected.Length; i++)
                {
                    Assert.Equal(expected[i], actual[i]);
                }
            }
        }
    }
}
