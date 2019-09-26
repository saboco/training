using Xunit;
using Training.Codility.TimeComplexity.PermMissingElem;

namespace Training.Codility.Tests.TimeComplexity
{
    public class PermMissingElemTest
    {
        [Theory]
        [InlineData(new[] { 2, 3, 1, 5 }, 4)]
        [InlineData(new[] { 4, 3, 1, 5 }, 2)]
        public void Should_return_the_missing_elem_in_permutation(int[] arr, int expected)
        {
            Assert.Equal(expected, Solution.Solve(arr));
        }
    }
}