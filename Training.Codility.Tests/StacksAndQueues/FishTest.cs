using NUnit.Framework;
using Training.Codility.StacksAndQueues.Fish;

namespace Training.Codility.Tests.StacksAndQueues
{
    public class FishTest
    {
        [TestCase(new[] {4, 3, 2, 1, 5}, new[] {0, 1, 0, 0, 0}, ExpectedResult = 2)]
        [TestCase(new[] {4, 3, 2, 1, 5}, new[] {1, 1, 0, 0, 0}, ExpectedResult = 1)]
        [TestCase(new[] {4, 3, 2, 1, 5, 6, 7, 8}, new[] {1, 1, 0, 0, 0, 0, 0, 0}, ExpectedResult = 4)]
        public int Should_Calculate_how_many_fish_are_alive(int[] a, int[] b)
        {
            return Solution.Solve(a, b);
        }
    }
}