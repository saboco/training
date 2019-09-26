using Xunit;

namespace Training.Codility.Tests.MaximumSliceProblem
{
    public class MaxDoubleSliceSumTest
    {
        [Theory]
        [InlineData(new[] { 3, 2, 6, -1, 4, 5, -1, 2 }, 17)]
        [InlineData(new[] { -8, 10, 20, -5, -7, -4 }, 30)]
        public void Should_return_the_max_sum_of_any_double_slice(int[] a, int expected)
        {
            Assert.Equal(expected, Codility.MaximumSliceProblem.MaxDoubleSliceSum.Solution.Solve(a));
        }
    }
}