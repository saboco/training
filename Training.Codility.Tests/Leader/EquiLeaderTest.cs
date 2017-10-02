using NUnit.Framework;

namespace Training.Codility.Tests.Leader
{
    public class EquiLeaderTest
    {
        [TestCase(new[] {4, 3, 4, 4, 4, 2}, ExpectedResult = 2)]
        [TestCase(new[] {1, 2, 3, 4, 5}, ExpectedResult = 0)]
        [TestCase(new[] {0, 0}, ExpectedResult = 1)]
        [TestCase(new[] {-1, 0}, ExpectedResult = 0)]
        [TestCase(new[] {-1, -1}, ExpectedResult = 1)]
        [TestCase(new[] {-1000000000, -1000000000}, ExpectedResult = 1)]
        public int Should_return_the_number_of_equi_leaders(int[] a)
        {
            return Codility.Leader.EquiLeader.Solution.Solve(a);
        }
    }
}