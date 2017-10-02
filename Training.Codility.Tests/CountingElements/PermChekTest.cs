using NUnit.Framework;

namespace Training.Codility.Tests.CountingElements
{
    public class PermChekTest
    {
        [TestCase(new[] { 1, 2, 3, 4, 5, 6 }, ExpectedResult = 1)]
        [TestCase(new[] { 1, 2, 4, 5, 6 }, ExpectedResult = 0)]
        [TestCase(new[] { 0, 3, 3, 4, 5, 6 }, ExpectedResult = 0)]
        public int Should_be_a_permutation(int[] arr)
        {
            return Codility.CountingElements.PermCheck.Solution.Solve(arr);
        }
    }
}