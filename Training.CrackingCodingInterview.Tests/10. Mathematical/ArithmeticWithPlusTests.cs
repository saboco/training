using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class ArithmeticWithPlusTests
    {
        [Theory]
        [InlineData(5, 2)]
        [InlineData(2, 5)]
        [InlineData(2, 0)]
        [InlineData(0, 2)]
        [InlineData(5, -2)]
        [InlineData(-2, 5)]
        [InlineData(-2, -5)]
        public void SubstractionTest(int a, int b)
        {
            var actual = ArithmeticWithPlus.Substract(a, b);
            var expected = a - b;
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(5, 2)]
        [InlineData(2, 5)]
        [InlineData(2, 0)]
        [InlineData(0, 2)]
        [InlineData(5, -2)]
        [InlineData(-2, 5)]
        [InlineData(-2, -5)]
        public void MultiplicationTest(int a, int b)
        {
            var actual = ArithmeticWithPlus.Multiply(a, b);
            var expected = a * b;
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(5, 2)]
        [InlineData(2, 5)]
        [InlineData(2, 0)]
        [InlineData(0, 2)]
        [InlineData(5, -2)]
        [InlineData(-2, 5)]
        [InlineData(-2, -5)]
        public void DivisionTest(int a, int b)
        {   
            if (b == 0)
            {
                Assert.Throws<InvalidOperationException>(() => ArithmeticWithPlus.Divide(a, b));
            }
            else
            {
                var actual = ArithmeticWithPlus.Divide(a, b);
                var expected = a / b;
                Assert.Equal(expected, actual);
            }
        }
    }
}
