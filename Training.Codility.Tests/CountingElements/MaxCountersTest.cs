using NUnit.Framework;

namespace Training.Codility.Tests.CountingElements
{
    public class MaxCountersTest
    {
        [TestCase(5, new[] { 3, 4, 4, 6, 1, 4, 4 }, ExpectedResult = new[] { 3, 2, 2, 4, 2 }, TestName = "Simple case")]
        [TestCase(5, new[] { 6, 6, 6, 6, 6, 6, 6, 6, 6 }, ExpectedResult = new[] { 0, 0, 0, 0, 0 }, TestName = "All max counters")]
        public int[] Should_treat_max_counters(int n, int[] arr)
        {
            return Codility.CountingElements.MaxCounters.Solution.Solve(n, arr);
        }
    }
}