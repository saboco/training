using Xunit;
using Training.Codility.PrefixSums.PassingCars;

namespace Training.Codility.Tests.PrefixSums
{
    public class PassingCarsTest
    {
        [Theory]
        [InlineData(new[] { 0, 1, 0, 1, 1 }, 5)]
        [InlineData(new[] { 1, 0, 1, 1 }, 2)]
        [InlineData(new[] { 0, 0, 1, 0, 1, 1 }, 8)]
        public void Should_count_the_number_of_passing_cars_on_the_road(int[] arr, int expected)
        {
            Assert.Equal(expected, Solution.Solve(arr));
        }
    }
}