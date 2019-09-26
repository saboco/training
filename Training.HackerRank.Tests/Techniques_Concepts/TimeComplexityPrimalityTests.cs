using Xunit;
using Training.HackerRank.Techniques_Concepts;

namespace Training.HackerRank.Tests.Techniques_Concepts
{
    public class TimeComplexityPrimalityTests
    {
        [Theory]
        [InlineData(1, false)]
        [InlineData(4, false)]
        [InlineData(5, true)]
        [InlineData(7, true)]
        [InlineData(9, false)]
        [InlineData(12, false)]
        [InlineData(16, false)]
        [InlineData(25, false)]
        [InlineData(36, false)]
        [InlineData(49, false)]
        [InlineData(64, false)]
        [InlineData(81, false)]
        [InlineData(100, false)]
        [InlineData(121, false)]
        [InlineData(144, false)]
        [InlineData(169, false)]
        [InlineData(196, false)]
        [InlineData(225, false)]
        [InlineData(256, false)]
        [InlineData(289, false)]
        [InlineData(324, false)]
        [InlineData(361, false)]
        [InlineData(400, false)]
        [InlineData(441, false)]
        [InlineData(484, false)]
        [InlineData(529, false)]
        [InlineData(576, false)]
        [InlineData(625, false)]
        [InlineData(676, false)]
        [InlineData(729, false)]
        [InlineData(784, false)]
        [InlineData(841, false)]
        [InlineData(907, true)]
        public void Should_return_if_a_number_is_prime(int n, bool expected)
        {
            Assert.Equal(expected, TimeComplexityPrimality.IsPrime(n));
        }
    }
}