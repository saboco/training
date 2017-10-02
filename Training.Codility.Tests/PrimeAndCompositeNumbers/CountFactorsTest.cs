using NUnit.Framework;
using Training.Codility.PrimeAndCompositeNumbers.CountFactors;

namespace Training.Codility.Tests.PrimeAndCompositeNumbers
{
    public class CountFactorsTest
    {
        [TestCase(7, ExpectedResult = 2)]
        [TestCase(24, ExpectedResult = 8)]
        public int Should_count_factors_of_n(int n)
        {
            return Solution.Solve(n);
        }
    }
}