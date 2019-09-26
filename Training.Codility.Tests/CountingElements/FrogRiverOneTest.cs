using Xunit;

namespace Training.Codility.Tests.CountingElements
{
    public class FrogRiverOneTest
    {
        // TODO: show good test name
        [Theory]
        [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, 5, 4)] //, TestName = "Easy cross"
        [InlineData(new[] { 1, 3, 1, 4, 2, 3, 5, 4 }, 5, 6)] //, TestName = "Cross in last minute"
        [InlineData(new[] { 1, 3, 1, 4, 2, 3, 4 }, 5, -1)] //, TestName = "Can't cross"
        public void Should_return_the_earliest_time_when_the_frog_can_traverse_the_river(int[] leaves, int x, int expected)
        {
            Assert.Equal(expected, Codility.CountingElements.FrogRiverOne.Solution.Solve(leaves, x));
        }
    }
}