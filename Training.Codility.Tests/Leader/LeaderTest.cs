using Xunit;

namespace Training.Codility.Tests.Leader
{
    public class LeaderTest
    {
        [Theory]
        [InlineData(new[] { 2, 6, 4, 1, 4, 4, 2, 4, 4 }, 4)]
        [InlineData(new[] { 6, 8, 4, 6, 8, 6, 6 }, 6)]
        [InlineData(new[] { 4, 6, 8, 6, 6, 6, 8 }, 6)]
        [InlineData(new[] { 4, 6, 6, 6, 6, 8, 8 }, 6)]
        [InlineData(new[] { 2 }, 2)]
        [InlineData(new[] { 2, 2 }, 2)]
        [InlineData(new[] { 1, 2, 3 }, -1)]
        [InlineData(new[] { 1, 2 }, -1)]
        [InlineData(new[] { 0, 0 }, 0)]
        [InlineData(new[] { -1, -1 }, -1)]
        public void Should_find_the_leader(int[] a, int expected)
        {
            Assert.Equal(expected, Codility.Leader.Leader.Find(a, 0, a.Length)[1]);
        }
    }
}