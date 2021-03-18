using Xunit;

namespace Training.Leetcode.Tests
{
    public class ShortestWordDistanceTests
    {
        [Theory]
        [InlineData(3, "coding", "practice", new[] { "practice", "makes", "perfect", "coding", "makes" })]
        [InlineData(1, "makes", "coding", new[] { "practice", "makes", "perfect", "coding", "makes" })]
        [InlineData(1, "makes", "coding", new[] { "coding", "makes" })]
        [InlineData(1, "b", "a", new[] { "a", "b" })]
        [InlineData(1, "a", "b", new[] { "a", "b" })]
        [InlineData(1, "b", "a", new[] { "a", "c", "b", "b", "a" })]
        [InlineData(3, "a", "d", new[] { "a", "b", "c", "d", "d" })]
        public static void ShortestWordDistanceTest(int expected, string word1, string word2, string[] words)
        {
            Assert.Equal(expected, ShortestWordDistance.ShortestDistance(words, word1, word2));
        }
    }
}
