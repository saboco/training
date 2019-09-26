using Xunit;
using Training.Codility.StacksAndQueues.Fish;

namespace Training.Codility.Tests.StacksAndQueues
{
    public class FishTest
    {
        [Theory]
        [InlineData(new[] { 4, 3, 2, 1, 5 }, new[] { 0, 1, 0, 0, 0 }, 2)]
        [InlineData(new[] { 4, 3, 2, 1, 5 }, new[] { 1, 1, 0, 0, 0 }, 1)]
        [InlineData(new[] { 4, 3, 2, 1, 5, 6, 7, 8 }, new[] { 1, 1, 0, 0, 0, 0, 0, 0 }, 4)]
        public void Should_Calculate_how_many_fish_are_alive(int[] a, int[] b, int expected)
        {
            Assert.Equal(expected, Solution.Solve(a, b));
        }
    }
}