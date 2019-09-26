using Xunit;

namespace Training.Codility.Tests.CountingElements
{
    public class PermChekTest
    {
        [Theory]
        [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, 1)]
        [InlineData(new[] { 1, 2, 4, 5, 6 }, 0)]
        [InlineData(new[] { 0, 3, 3, 4, 5, 6 }, 0)]
        public void Should_be_a_permutation(int[] arr, int expected)
        {
            Assert.Equal(expected, Codility.CountingElements.PermCheck.Solution.Solve(arr));
        }
    }
}