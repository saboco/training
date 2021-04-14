using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class StringIsARotationTests
    {
        [Theory]
        [InlineData("", "", true)]
        [InlineData("", null, false)]
        [InlineData(null, "", false)]
        [InlineData(null, null, false)]
        [InlineData("waterbottle", "erbottlewat", true)]
        [InlineData("water", "erwat", true)]
        [InlineData("water", "erwatt", false)]
        [InlineData("water", "water", true)]
        public void IsARotationTest(string s1, string s2, bool expected)
        {
            var actual = StringIsARotation.IsARotation(s1,s2);
            Assert.Equal(expected,actual);
        }
    }
}
