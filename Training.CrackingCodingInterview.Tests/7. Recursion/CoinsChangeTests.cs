using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class CoinsChangeTests
    {
        [Theory]
        [InlineData(5, 2)]
        [InlineData(4, 1)]
        [InlineData(3, 1)]
        [InlineData(2, 1)]
        [InlineData(1, 1)]
        [InlineData(10, 4)]
        public void CountWaysTest(int n, int expected)
        {
            var actual = CoinsChange.CountWays(n);
            Assert.Equal(expected, actual);
        }
    }
}
