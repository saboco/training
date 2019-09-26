using Xunit;
using Training.HackerRank.Techniques_Concepts;

namespace Training.HackerRank.Tests.Techniques_Concepts
{
    public class BitManipulationLonelyIntegerTests
    {
        [Theory]
        [InlineData(new[] { 0, 0, 1, 2, 1 }, 2)]
        [InlineData(new[] { 0, 0, 1, 2, 1, 0, 2 }, 0)]
        public void Should_return_the_integer_that_does_not_have_a_pair(int[] arr, int expected)
        {
            Assert.Equal(expected, BitManipulationLonelyInteger.GetLonleyInteger(arr));
        }
    }
}