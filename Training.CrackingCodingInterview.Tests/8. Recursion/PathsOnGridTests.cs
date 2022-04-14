using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class PathsOnGridTests
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 6)]
        [InlineData(8, 3432)]
        public void CountPathsTest(int n, int expectedCount)
        {
            var count = PathsOnGrid.CountPaths(n);
            Assert.Equal(expectedCount, count);
        }

        [Theory]
        [InlineData(1, new[] { 0 })]
        [InlineData(0, new[] { 1 })]
        [InlineData(2, new[] { 0, 0 }, new[] { 0, 0 })]
        [InlineData(1, new[] { 0, 1 }, new[] { 0, 0 })]
        [InlineData(6, new[] { 0, 0, 0 }, new[] { 0, 0, 0 }, new[] { 0, 0, 0 })]
        [InlineData(2, new[] { 0, 0, 0 }, new[] { 0, 1, 0 }, new[] { 0, 0, 0 })]
        [InlineData(3432
            ,new[] { 0, 0, 0, 0, 0, 0, 0, 0 }
            ,new[] { 0, 0, 0, 0, 0, 0, 0, 0 }
            ,new[] { 0, 0, 0, 0, 0, 0, 0, 0 }
            ,new[] { 0, 0, 0, 0, 0, 0, 0, 0 }
            ,new[] { 0, 0, 0, 0, 0, 0, 0, 0 }
            ,new[] { 0, 0, 0, 0, 0, 0, 0, 0 }
            ,new[] { 0, 0, 0, 0, 0, 0, 0, 0 }
            ,new[] { 0, 0, 0, 0, 0, 0, 0, 0 })]
        [InlineData(2592
            , new[] { 0, 0, 0, 0, 0, 0, 0, 0 }
            , new[] { 0, 0, 0, 0, 0, 0, 0, 0 }
            , new[] { 0, 0, 0, 0, 0, 0, 0, 0 }
            , new[] { 0, 0, 0, 0, 0, 1, 0, 0 }
            , new[] { 0, 0, 0, 0, 0, 0, 0, 0 }
            , new[] { 0, 0, 0, 0, 0, 0, 0, 0 }
            , new[] { 0, 0, 0, 0, 0, 0, 0, 0 }
            , new[] { 0, 0, 0, 0, 0, 0, 0, 0 })]
        public void CountPathsWithObstaclesTest(int expectedCount, params int[][] grid)
        {
            var count = PathsOnGrid.CountPaths(grid);
            Assert.Equal(expectedCount, count);
        }
    }
}
