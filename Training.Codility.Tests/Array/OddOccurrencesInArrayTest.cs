using Xunit;
using Training.Codility.Array.OddOccurrencesInArray;

namespace Training.Codility.Tests.Array
{
    public class OddOccurrencesInArray
    {
        [Theory]
        [InlineData(new[] { 9, 3, 9, 3, 9, 7, 9 }, 7)]
        [InlineData(new[] { 9, 3, 9, 3, 9, 10, 9 }, 10)]
        [InlineData(new[] { 9, 3, 9, 3, 9, 1, 9 }, 1)]
        public void Should_return_the_odd_in_array(int[] arr, int expected)
        {
            Assert.Equal(expected, Solution.Solve(arr));
        }
    }
}