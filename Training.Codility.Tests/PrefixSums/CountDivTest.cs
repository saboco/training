using NUnit.Framework;

namespace Training.Codility.Tests.PrefixSums
{
    public class CountDivTest
    {
        [TestCase(0, 12, 12, ExpectedResult = 2)]
        [TestCase(9, 11, 2, ExpectedResult = 1)]
        [TestCase(9, 11, 3, ExpectedResult = 1)]
        [TestCase(9, 11, 4, ExpectedResult = 0)]
        [TestCase(9, 11, 5, ExpectedResult = 1)]
        [TestCase(9, 11, 6, ExpectedResult = 0)]
        [TestCase(9, 11, 7, ExpectedResult = 0)]
        [TestCase(9, 11, 8, ExpectedResult = 0)]
        [TestCase(9, 11, 9, ExpectedResult = 1)]
        [TestCase(9, 11, 10, ExpectedResult = 1)]
        [TestCase(9, 11, 11, ExpectedResult = 1)]
        [TestCase(9, 11, 1, ExpectedResult = 3)]
        [TestCase(1, 5, 1, ExpectedResult = 5)]
        [TestCase(0, 5, 1, ExpectedResult = 6)]
        public int Should_compute_the_number_of_integers_divisible_by_k_in_range(int a, int b, int k)
        {
            return Codility.PrefixSums.CountingDiv.Solution.Solve(a, b, k);
        }
    }
}