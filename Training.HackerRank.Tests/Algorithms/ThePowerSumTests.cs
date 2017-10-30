using NUnit.Framework;
using Training.HackerRank.Algorithms;

namespace Training.HackerRank.Tests.Algorithms
{
    public class ThePowerSumTests
    {
        [Ignore("Not implemented")]
        [TestCase(10, 2, ExpectedResult = 1)]
        [TestCase(100, 2, ExpectedResult = 2)]
        public long Should_return_the_number_of_ways_x_can_be_represented_as_powers_of_n(int x, int n)
        {
            return ThePowerSum.GetNumberOfRepresentationsFor(x, n);
        }
    }
}