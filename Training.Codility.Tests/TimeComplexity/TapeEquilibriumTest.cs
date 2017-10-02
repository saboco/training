using NUnit.Framework;
using Training.Codility.TimeComplexity.TapeEquilibrium;

namespace Training.Codility.Tests.TimeComplexity
{
    public class TapeEquilibriumTest

    {
        [TestCase(new[] { 3, 1, 2, 4, 3 }, ExpectedResult = 1)]
        [TestCase(new[] { 1, 20001 }, ExpectedResult = 20000)]
        [TestCase(new[] { 20000, 50000 }, ExpectedResult = 30000)]
        [TestCase(new[] { -20000, 5000 }, ExpectedResult = 25000)]
        [TestCase(new[] { -20000, -5000 }, ExpectedResult = 15000)]
        [TestCase(new []{ -10, -20, -30, -40, 100 }, ExpectedResult = 20)]
        public int Should_return_the_minimal_diference_that_can_be_achived(int[] arr)
        {
            return Solution.Solve(arr);
        }
    }
}