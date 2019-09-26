using Xunit;
using Training.HackerRank.Techniques_Concepts;

namespace Training.HackerRank.Tests.Techniques_Concepts
{
    public class RecursionDavisStaircaseTests
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 4)]
        [InlineData(7, 44)]
        public void Should_return_the_number_of_ways_davis_can_clilmb_an_staircase(int staircaseSize, long expected)
        {
            Assert.Equal(expected, RecursionDavisStaircase.GetNumberOfWays(staircaseSize));
        }
    }
}