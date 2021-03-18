using Xunit;

namespace Training.Leetcode.Tests
{
    public class SingleNumberTests
    {
        [Theory]
        [InlineData(1, new[] { 2, 2, 1 })]
        [InlineData(4, new[] { 4, 1, 2, 1, 2 })]
        public void SingleNumberTest(int expected, int[] nums)
        {
            Assert.Equal(expected, SingleNumber.Single(nums));
            Assert.Equal(expected, SingleNumber.SingleConstantMemory(nums));
        }
    }
}
