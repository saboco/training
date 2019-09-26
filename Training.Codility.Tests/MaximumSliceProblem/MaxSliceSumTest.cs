using Xunit;

namespace Training.Codility.Tests.MaximumSliceProblem
{
    public class MaxSliceSumTest
    {
        [Theory]
        [InlineData(new[] {3, 2, -6, 4, 0}, 5)]
        [InlineData(new[] {-10}, -10)]
        [InlineData(new int[0], 0)]
        public void Should_return_the_maximum_sum_of_a_slice(int[] a, int expected)
        {
            Assert.Equal(expected, Codility.MaximumSliceProblem.MaxSliceSum.Solution.Solve(a));
        }
    }
}