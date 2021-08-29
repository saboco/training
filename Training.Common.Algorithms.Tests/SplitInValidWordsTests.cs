using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class SplitInValidWordsTests
    {
        [Theory]
        [InlineData("catsanddog",
            new[] { "cat", "cats", "and", "sand", "dog" },
            new[] { "cat", "sand", "dog" },
            new[] { "cats", "and", "dog" })]
        [InlineData("pineapplepenapple",
            new[] { "apple", "pen", "applepen", "pine", "pineapple" },
            new[] { "pine", "apple", "pen", "apple" },
            new[] { "pine", "applepen", "apple" },
            new[] { "pineapple", "pen", "apple" })]
        public void SplitInValidWordsTest(string word, string[] dico, params string[][] expected)
        {
            var words = SplitInValidWords.SplitValidWords(word, dico).ToArray();
            Common.AssertEqual(expected, words);
        }

        List<Func<string, string[], int>> _actions = new List<Func<string, string[], int>>
        {
            SplitInValidWords.Count,
            SplitInValidWords.CountWays,
            SplitInValidWords.CountWaysMemo,
            //SplitInValidWords.CountWaysDp
        };

        [Theory]
        [InlineData("catsanddog",
            new[] { "cat", "cats", "and", "sand", "dog" }, 2)]
        [InlineData("pineapplepenapple",
            new[] { "apple", "pen", "applepen", "pine", "pineapple" }, 3)]
        [InlineData("aaaaaaaa",
            new[] { "a", "aa", "aaa", "aaaa", "aaaaa", "aaaaaa", "aaaaaaa", "aaaaaaaa" }, 128)]
        public void CountTest(string word, string[] dico, int expectedCount)
        {
            foreach (var action in _actions)
            {
                var count = action.Invoke(word, dico);
                Assert.Equal(expectedCount, count);
            }
        }

        [Theory]
        [InlineData("catsanddog",
            new[] { "cat", "cats", "and", "sand", "dog" }, 2,
            new[] { "dog", "and", "cats" },
            new[] { "dog", "sand", "cat" })]
        [InlineData("pineapplepenapple",
            new[] { "apple", "pen", "applepen", "pine", "pineapple" }, 3,
            new[] { "apple", "pen", "apple", "pine" },
            new[] { "apple", "pen", "pineapple" },
            new[] { "apple", "applepen", "pine" })]
        public void CountPrintTest(string word, string[] dico, int expectedCount, params string[][] expectedWords)
        {
            var (count, allWords) = SplitInValidWords.CountPrint(word, dico);
            Assert.Equal(expectedCount, count);
            Common.AssertEqual(expectedWords, allWords);
        }
    }
}
