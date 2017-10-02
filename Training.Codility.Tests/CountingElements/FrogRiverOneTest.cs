using NUnit.Framework;

namespace Training.Codility.Tests.CountingElements
{
    public class FrogRiverOneTest
    {
        [TestCase(new[] { 1, 2, 3, 4, 5, 6 }, 5, ExpectedResult = 4, TestName = "Easy cross")]
        [TestCase(new[] { 1, 3, 1, 4, 2, 3, 5, 4 }, 5, ExpectedResult = 6, TestName = "Cross in last minute")]
        [TestCase(new[] { 1, 3, 1, 4, 2, 3, 4 }, 5, ExpectedResult = -1, TestName = "Can't cross")]
        public int Should_return_the_earliest_time_when_the_frog_can_traverse_the_river(int[] leaves, int x)
        {
            return Codility.CountingElements.FrogRiverOne.Solution.Solve(leaves, x);
        }
    }
}