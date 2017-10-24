using NUnit.Framework;
using Training.HackerRank.Techniques_Concepts;

namespace Training.HackerRank.Tests.Techniques_Concepts
{
    public class RecursionDavisStaircaseTests
    {
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 2)]
        [TestCase(3, ExpectedResult = 4)]
        [TestCase(7, ExpectedResult = 44)]
        public long Should_return_the_number_of_ways_davis_can_clilmb_an_staircase(int staircaseSize)
        {
            return RecursionDavisStaircase.GetNumberOfWays(staircaseSize);
        }
    }
}