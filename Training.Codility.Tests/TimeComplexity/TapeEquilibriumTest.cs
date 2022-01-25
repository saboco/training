using Xunit;
using Training.Codility.TimeComplexity.TapeEquilibrium;

namespace Training.Codility.Tests.TimeComplexity
{
    public class TapeEquilibriumTest

    {
        [Theory]
        [InlineData(new[] { 3, 1, 2, 4, 3 }, 1)]
        [InlineData(new[] { 1, 20001 }, 20000)]
        [InlineData(new[] { 20000, 50000 }, 30000)]
        [InlineData(new[] { -20000, 5000 }, 25000)]
        [InlineData(new[] { -20000, -5000 }, 15000)]
        [InlineData(new[] { -10, -20, -30, -40, 100 }, 20)]
        public void Should_return_the_minimal_diference_that_can_be_achived(int[] arr, int expected)
        {
            Assert.Equal(expected, Solution.Solve(arr));
        }
    }
}