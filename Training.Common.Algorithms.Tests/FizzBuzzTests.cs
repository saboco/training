using Xunit;

namespace Training.Common.Algorithms.Tests
{
    /*
    "Write a program that prints the numbers from 1 to 100. 
     But for multiples of three print “Fizz” instead of the number and for the multiples of five print “Buzz”. 
     For numbers which are multiples of both three and five print “FizzBuzz”."
    */
    
    public class FizBuzzTests
    {
        [Theory]
        [InlineData(3)]
        [InlineData(6)]
        [InlineData(12)]
        public void Should_return_Fizz_when_number_is_multiple_of_three(int n)
        {
            Assert.Equal("Fizz", FizzBuzz.GetFizzBuzz(n));
        }

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(20)]
        public void Should_return_Buzz_when_number_is_multiple_of_five(int n)
        {
            Assert.Equal("Buzz", FizzBuzz.GetFizzBuzz(n));
        }

        [Theory]
        [InlineData(15)]
        [InlineData(30)]
        [InlineData(45)]
        public void Should_return_FizzBuzz_when_number_is_multiple_of_three_and_five(int n)
        {
            Assert.Equal("FizzBuzz", FizzBuzz.GetFizzBuzz(n));
        }
    }
}
