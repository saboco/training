using Xunit;

namespace Training.Codility.Tests.Sorting
{
    public class DistinctTest
    {
        [Theory]
        [InlineData(new[] { 2, 3, 1, 3, 3, 1, 2 }, 3)]
        [InlineData(new[] { 2, 3, 1, 3, 3, 1, 2, 4, 1, 9, 1, 10 }, 6)]
        [InlineData(new int[0], 0)]
        public void Should_retunr_the_number_of_distinct_values_in_a_array(int[] a, int expected)
        {
            Assert.Equal(expected, Codility.Sorting.Distinct.Solution.Solve(a));
        }
    }
}