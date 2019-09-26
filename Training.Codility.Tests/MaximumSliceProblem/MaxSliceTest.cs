using Xunit;

namespace Training.Codility.Tests.MaximumSliceProblem
{
    public class MaxSliceTest
    {
        [Theory]
        [InlineData(new[] { 5, -7, 3, 5, -2, 4, -1 }, 10)]
        [InlineData(new[] { -5, -7, -3, -5, -2, -4, -1 }, 0)]
        public void Should_return_the_max_slice(int[] a, int expected)
        {
            Assert.Equal(expected, Codility.MaximumSliceProblem.GoldenMaxSlice.Solve(a));
        }

        [Theory]
        [InlineData(new[] { 5, -7, 3, 5, -2, 4, -1 }, 10)]
        [InlineData(new[] { -5, -7, -3, -5, -2, -4, -1 }, 0)]
        public void Should_slowly_return_the_max_slice(int[] a, int expected)
        {
            Assert.Equal(expected, Codility.MaximumSliceProblem.SlowMaximalSlice.Solve(a));
        }
    }
}