using Xunit;

namespace Training.Codility.Tests.CountingElements
{
    public class MaxCountersTest
    {
        [Theory]
        [InlineData(5, new[] { 3, 4, 4, 6, 1, 4, 4 }, new[] { 3, 2, 2, 4, 2 })] // , TestName = "Simple case"
        [InlineData(5, new[] { 6, 6, 6, 6, 6, 6, 6, 6, 6 }, new[] { 0, 0, 0, 0, 0 })] // , TestName = "All max counters"
        public void Should_treat_max_counters(int n, int[] arr, int[] expected)
        {
            Assert.Equal(expected, Codility.CountingElements.MaxCounters.Solution.Solve(n, arr));
        }
    }
}