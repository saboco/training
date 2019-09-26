using Xunit;
using Training.Codility.Array.CyclicRotation;

namespace Training.Codility.Tests.Array
{
    public class CyclicRotation
    {
        [Theory]
        [InlineData(new[] { 3, 8, 9, 7, 6 }, 1, new[] { 6, 3, 8, 9, 7 })]
        [InlineData(new[] { 3, 8, 9, 7, 6 }, 2, new[] { 7, 6, 3, 8, 9 })]
        [InlineData(new[] { 3, 8, 9, 7, 6 }, 3, new[] { 9, 7, 6, 3, 8 })]
        [InlineData(new[] { 3, 8, 9, 7, 6 }, 4, new[] { 8, 9, 7, 6, 3 })]
        public void Should_rotate_the_array(int[] arr, int k, int[] expected)
        {
            Assert.Equal(expected, Solution.Solve(arr, k));
        }
    }
}