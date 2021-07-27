using System;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class SwapBitsTests
    {
        [Theory]
        [InlineData("101101", "11110")]
        [InlineData("1", "10")]
        [InlineData("0", "0")]
        public void SwapOddEvenBits(string xB, string expected)
        {
            var x = Convert.ToInt32(xB, 2);
            var xp = SwapBits.SwapOddEvenBits(x);
            var actual = Convert.ToString(xp, 2);
            Assert.Equal(expected, actual);
        }
    }
}
