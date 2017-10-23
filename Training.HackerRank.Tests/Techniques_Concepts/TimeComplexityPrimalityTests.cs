using NUnit.Framework;
using Training.HackerRank.Techniques_Concepts;

namespace Training.HackerRank.Tests.Techniques_Concepts
{
    public class TimeComplexityPrimalityTests
    {
        [Test]
        public void Should_return_if_a_number_is_prime()
        {
            Assert.IsFalse(TimeComplexityPrimality.IsPrime(12));
            Assert.IsTrue(TimeComplexityPrimality.IsPrime(5));
            Assert.IsTrue(TimeComplexityPrimality.IsPrime(7));
        }

        [TestCase(1, ExpectedResult = false)]
        [TestCase(4, ExpectedResult = false)]
        [TestCase(9, ExpectedResult = false)]
        [TestCase(16, ExpectedResult = false)]
        [TestCase(25, ExpectedResult = false)]
        [TestCase(36, ExpectedResult = false)]
        [TestCase(49, ExpectedResult = false)]
        [TestCase(64, ExpectedResult = false)]
        [TestCase(81, ExpectedResult = false)]
        [TestCase(100, ExpectedResult = false)]
        [TestCase(121, ExpectedResult = false)]
        [TestCase(144, ExpectedResult = false)]
        [TestCase(169, ExpectedResult = false)]
        [TestCase(196, ExpectedResult = false)]
        [TestCase(225, ExpectedResult = false)]
        [TestCase(256, ExpectedResult = false)]
        [TestCase(289, ExpectedResult = false)]
        [TestCase(324, ExpectedResult = false)]
        [TestCase(361, ExpectedResult = false)]
        [TestCase(400, ExpectedResult = false)]
        [TestCase(441, ExpectedResult = false)]
        [TestCase(484, ExpectedResult = false)]
        [TestCase(529, ExpectedResult = false)]
        [TestCase(576, ExpectedResult = false)]
        [TestCase(625, ExpectedResult = false)]
        [TestCase(676, ExpectedResult = false)]
        [TestCase(729, ExpectedResult = false)]
        [TestCase(784, ExpectedResult = false)]
        [TestCase(841, ExpectedResult = false)]
        [TestCase(907, ExpectedResult = true)]
        public bool Should_return_if_a_number_is_prime(int n)
        {
            return TimeComplexityPrimality.IsPrime(n);
        }
    }
}