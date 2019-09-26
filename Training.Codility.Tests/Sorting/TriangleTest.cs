using Xunit;

namespace Training.Codility.Tests.Sorting
{
    public class TriangleTest
    {
        [Theory]
        [InlineData(new[] { 10, 2, 5, 1, 8, 20 }, 1)]
        [InlineData(new[] { 10, 50, 5, 1 }, 0)]
        [InlineData(new[] { 2147248346, 247483647, 2147248345 }, 1)]
        public void Should_determine_if_a_triangle_can_be_build(int[] a, int expected)
        {
            Assert.Equal(expected, Codility.Sorting.Triangle.Solution.Solve(a));
        }
    }
}