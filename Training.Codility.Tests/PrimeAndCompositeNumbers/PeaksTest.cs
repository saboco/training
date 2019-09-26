using Xunit;

namespace Training.Codility.Tests.PrimeAndCompositeNumbers
{
    public class PeaksTest
    {
        [Theory]
        [InlineData(new[] { 1, 2, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2 }, 3)]
        [InlineData(new[] { 1, 2, 3, 1 }, 1)]
        [InlineData(new[] { 0, 1, 0, 0, 1, 0, 0, 1, 0 }, 3)]
        [InlineData(new[] { 0, 1000000000 }, 0)]
        [InlineData(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }, 0)]
        [InlineData(new[] { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 }, 4)]
        public void Should_return_the_maximum_number_of_blocks_that_arra_a_can_be_divided(int[] a, int expected)
        {
            Assert.Equal(expected, Codility.PrimeAndCompositeNumbers.Peaks.Solution.Solve(a));
        }
    }
}