using Xunit;

namespace Training.Codility.Tests.Leader
{
    public class EquiLeaderTest
    {
        [Theory]
        [InlineData(new[] {4, 3, 4, 4, 4, 2}, 2)]
        [InlineData(new[] {1, 2, 3, 4, 5}, 0)]
        [InlineData(new[] {0, 0}, 1)]
        [InlineData(new[] {-1, 0}, 0)]
        [InlineData(new[] {-1, -1}, 1)]
        [InlineData(new[] {-1000000000, -1000000000}, 1)]
        public void Should_return_the_number_of_equi_leaders(int[] a, int expected)
        {
            Assert.Equal(expected, Codility.Leader.EquiLeader.Solution.Solve(a));
        }
    }
}