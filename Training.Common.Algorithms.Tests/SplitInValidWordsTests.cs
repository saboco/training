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
        public void SplitInValidWordsTest(string word, string[] dico, params string[][] expected)
        {
            var words = SplitInValidWords.SplitValidWords(word, dico).ToArray();
            Common.AssertEqual(expected, words);
        }
    }
}
