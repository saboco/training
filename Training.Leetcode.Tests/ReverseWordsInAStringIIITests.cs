using Xunit;

namespace Training.Leetcode.Tests
{
    public class ReverseWordsInAStringIIITests
    {
        [Theory]
        [InlineData("s'teL ekat edoCteeL tsetnoc", "Let's take LeetCode contest")]
        public void ReverseWordsInAStringIIITest(string expected, string s)
        {
            Assert.Equal(expected, ReverseWordsInAStringIII.ReverseWords(s));
        }
    }
}
