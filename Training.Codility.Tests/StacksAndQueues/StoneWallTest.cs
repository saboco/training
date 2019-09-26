using Xunit;

namespace Training.Codility.Tests.StacksAndQueues
{
    public class StoneWallTest
    {
        [Theory]
        [InlineData(new[] { 8, 8, 5, 7, 9, 8, 7, 4, 8 }, 7)]
        public void Should_return_the_minimum_number_of_rectagles_to_cover_manhattan_skyline(int[] a, int expected)
        {
            Assert.Equal(expected, Codility.StacksAndQueues.StoneWall.Solution.Solve(a));
        }
    }
}