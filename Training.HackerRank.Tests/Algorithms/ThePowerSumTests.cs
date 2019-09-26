using Xunit;
using Training.HackerRank.Algorithms;

namespace Training.HackerRank.Tests.Algorithms
{
    public class ThePowerSumTests
    {
        [Theory]
        [InlineData(10, 2, 1)]
        [InlineData(100, 2, 3)]
        [InlineData(1, 2, 1)]
        [InlineData(3, 2, 0)]
        [InlineData(4, 2, 1)]
        public void Should_return_the_number_of_ways_x_can_be_represented_as_powers_of_n(int x, int n, long expected)
        {
            Assert.Equal(expected, ThePowerSum.GetNumberOfRepresentationsFor(x, n));
        }
    }
}