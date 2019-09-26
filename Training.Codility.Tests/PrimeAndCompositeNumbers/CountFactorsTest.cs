using Xunit;
using Training.Codility.PrimeAndCompositeNumbers.CountFactors;

namespace Training.Codility.Tests.PrimeAndCompositeNumbers
{
    public class CountFactorsTest
    {
        [Theory]
        [InlineData(7, 2)]
        [InlineData(24, 8)]
        public void Should_count_factors_of_n(int n, int expected)
        {
            Assert.Equal(expected, Solution.Solve(n));
        }
    }
}