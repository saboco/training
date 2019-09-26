using Xunit;

namespace Training.Codility.Tests.PrefixSums
{
    public class MinAvgTwoSliceTest
    {
        [Theory]
        [InlineData(new[] { 4, 2, 2, 5, 1, 5, 8 }, 1)]
        [InlineData(new[] { 2, 2, 4, 5, 1, 5, 8 }, 0)]
        [InlineData(new[] { 1, 1, 1, 1, 1, 1, 1 }, 0)]
        [InlineData(new[] { 1, 1 }, 0)]
        [InlineData(new[] { 1, 4 }, 0)]
        [InlineData(new[] { 1, 4, 2, 1 }, 2)]
        [InlineData(new[] { 1, 2, 3, 4 }, 0)]
        [InlineData(new[] { 10000, -10000 }, 0)]
        [InlineData(new[] { -3, -5, -8, -4, -10 }, 2)] //, TestName = "All negatives"
        public void Should_find_the_minimal_average_of_any_slice_containing_at_least_two_elements(int[] arr, int expected)
        {
            Assert.Equal(expected, Codility.PrefixSums.MinAvgTwoSlice.Solution.Solve(arr));
        }
    }
}