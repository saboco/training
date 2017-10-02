using NUnit.Framework;

namespace Training.Codility.Tests.PrimeAndCompositeNumbers
{
    public class PeaksTest
    {
        [TestCase(new[] {1, 2, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2}, ExpectedResult = 3)]
        [TestCase(new[] {1, 2, 3, 1}, ExpectedResult = 1)]
        [TestCase(new[] {0, 1, 0, 0, 1, 0, 0, 1, 0}, ExpectedResult = 3)]
        [TestCase(new[] {0, 1000000000}, ExpectedResult = 0)]
        [TestCase(new[] {0, 1, 2, 3, 4, 5, 6, 7, 8}, ExpectedResult = 0)]
        [TestCase(new[] {0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1}, ExpectedResult = 4)]
        public int Should_return_the_maximum_number_of_blocks_that_arra_a_can_be_divided(int[] a)
        {
            return Codility.PrimeAndCompositeNumbers.Peaks.Solution.Solve(a);
        }
    }
}