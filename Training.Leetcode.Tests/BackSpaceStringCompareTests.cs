using Xunit;

namespace Training.Leetcode.Tests
{
    public class BackSpaceStringCompareTests
    {
        [Theory]
        [InlineData(false, "ab##", "c#d#c")]
        [InlineData(true, "ab##", "c#d#")]
        [InlineData(true, "a##c", "#a#c")]
        public void BackSpaceStringCompareTest(bool expected, string s, string t)
        {
            Assert.Equal(expected, BackSpaceStringCompare.BackspaceCompare0(s, t));
            Assert.Equal(expected, BackSpaceStringCompare.BackspaceCompare(s, t));
        }
    }
}
