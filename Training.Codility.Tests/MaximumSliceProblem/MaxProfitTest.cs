using Xunit;

namespace Training.Codility.Tests.MaximumSliceProblem
{
    public class MaxProfitTest
    {
        [Theory]
        [InlineData(new[] { 23171, 21011, 21123, 21366, 21013, 21367 }, 356)]
        [InlineData(new[] { 23171, 21011, 21123, 21367, 21013, 21366 }, 356)]
        [InlineData(new[] { 21011, 23171, 21123, 21367, 21013, 21366 }, 2160)]
        public void Should_return_the_max_profit(int[] a, int expected)
        {
            Assert.Equal(expected, Codility.MaximumSliceProblem.MaxProfit.Solution.Solve(a));
        }
    }
}