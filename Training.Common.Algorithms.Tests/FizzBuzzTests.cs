using NUnit.Framework;

namespace Training.Common.Algorithms.Tests
{
    /*
    "Write a program that prints the numbers from 1 to 100. 
     But for multiples of three print “Fizz” instead of the number and for the multiples of five print “Buzz”. 
     For numbers which are multiples of both three and five print “FizzBuzz”."
    */
    [TestFixture]
    public class FizBuzzTests
    {
        [TestCase(3)]
        [TestCase(6)]
        [TestCase(12)]
        public void Should_return_Fizz_when_number_is_multiple_of_three(int n)
        {
            Assert.AreEqual("Fizz", FizzBuzz.GetFizzBuzz(n));
        }


        [TestCase(5)]
        [TestCase(10)]
        [TestCase(20)]
        public void Should_return_Buzz_when_number_is_multiple_of_five(int n)
        {
            Assert.AreEqual("Buzz", FizzBuzz.GetFizzBuzz(n));
        }

        [TestCase(15)]
        [TestCase(30)]
        [TestCase(45)]
        public void Should_return_FizzBuzz_when_number_is_multiple_of_three_and_five(int n)
        {
            Assert.AreEqual("FizzBuzz", FizzBuzz.GetFizzBuzz(n));
        }
    }
}
