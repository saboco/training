using Xunit;

namespace Training.Codility.Tests.PrefixSums
{
    public class CountDivTest
    {
        [Theory]
        [InlineData(0, 12, 12, 2)]
        [InlineData(9, 11, 2, 1)]
        [InlineData(9, 11, 3, 1)]
        [InlineData(9, 11, 4, 0)]
        [InlineData(9, 11, 5, 1)]
        [InlineData(9, 11, 6, 0)]
        [InlineData(9, 11, 7, 0)]
        [InlineData(9, 11, 8, 0)]
        [InlineData(9, 11, 9, 1)]
        [InlineData(9, 11, 10, 1)]
        [InlineData(9, 11, 11, 1)]
        [InlineData(9, 11, 1, 3)]
        [InlineData(1, 5, 1, 5)]
        [InlineData(0, 5, 1, 6)]
        public void Should_compute_the_number_of_integers_divisible_by_k_in_range(int a, int b, int k, int expected)
        {
            Assert.Equal(expected, Codility.PrefixSums.CountingDiv.Solution.Solve(a, b, k));
        }
    }
}