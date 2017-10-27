using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Training.Common.Algorithms.Tests
{
    public class AnagramsTests
    {
        [TestCase("ab", new[] {"ab", "ba"})]
        [TestCase("abc", new[] {"abc", "bac", "cba", "acb", "bca", "cab"})]
        [TestCase("aab", new[] {"aab", "baa", "aba"})]
        public void Should_return_all_anagrams_in_word(string s, string[] expected)
        {
            var anagrams = Anagrams.GetAnagramsIn(s).ToArray();
            AssertAnagrams(expected, anagrams);
        }

        private static void AssertAnagrams(IEnumerable<string> expected, string[] actual)
        {
            foreach (var expectedAnagram in expected)
            {
                Assert.IsTrue(actual.Contains(expectedAnagram));
            }
        }
    }
}