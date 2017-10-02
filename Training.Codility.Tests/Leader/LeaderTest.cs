using NUnit.Framework;

namespace Training.Codility.Tests.Leader
{
    public class LeaderTest
    {
        [TestCase(new[] {2, 6, 4, 1, 4, 4, 2, 4, 4}, ExpectedResult = 4)]
        [TestCase(new[] {6, 8, 4, 6, 8, 6, 6}, ExpectedResult = 6)]
        [TestCase(new[] {4, 6, 8, 6, 6, 6, 8}, ExpectedResult = 6)]
        [TestCase(new[] {4, 6, 6, 6, 6, 8, 8}, ExpectedResult = 6)]
        [TestCase(new[] {2}, ExpectedResult = 2)]
        [TestCase(new[] {2, 2}, ExpectedResult = 2)]
        [TestCase(new[] {1, 2, 3}, ExpectedResult = -1)]
        [TestCase(new[] {1, 2}, ExpectedResult = -1)]
        [TestCase(new[] {0, 0}, ExpectedResult = 0)]
        [TestCase(new[] {-1, -1}, ExpectedResult = -1)]
        public int Should_find_the_leader(int[] a)
        {
            return Codility.Leader.Leader.Find(a, 0, a.Length)[1];
        }
    }
}