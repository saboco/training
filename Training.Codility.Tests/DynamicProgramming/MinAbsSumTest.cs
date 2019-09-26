using Xunit;

namespace Training.Codility.Tests.DynamicProgramming
{
    public class MinAbsSumTest
    {
        [Theory]
        [InlineData(new[] { 1, 5, 2, -2 }, 4, 0)]
        public void Should_find_the_lowest_absolute_sum_of_elements(int[] arr, int n, int expected)
        {
            Assert.Equal(expected, Codility.DynamicProgramming.MinAbsSum.Solution.Solve(arr, n));
        }
    }
}