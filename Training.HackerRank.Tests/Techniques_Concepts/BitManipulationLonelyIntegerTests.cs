using NUnit.Framework;
using Training.HackerRank.Techniques_Concepts;

namespace Training.HackerRank.Tests.Techniques_Concepts
{
    public class BitManipulationLonelyIntegerTests
    {
        [TestCase(new []{0,0,1,2,1}, ExpectedResult = 2)]
        [TestCase(new []{0,0,1,2,1,0,2}, ExpectedResult = 0)]
        public int Should_return_the_integer_that_does_not_have_a_pair(int[] arr)
        {
            return BitManipulationLonelyInteger.GetLonleyInteger(arr);
        }
    }
}