using System.Runtime.Remoting;
using NUnit.Framework;
using Training.Tests.Common;

namespace Training.DataStructures.Tests
{
    [TestFixture]
    public class TriesTest
    {
        [Test]
        public void Should_construct_a_valid_tries_when_adding_strings()
        {
            var input = new[] { "CAR", "CARD", "CARDAN" };
            var printer = new FakePrinter("{0:#}");
            var tries = new Tries();
            foreach (var s in input)
            {
                tries.Add(s);
            }
            tries.Print(printer);
            Assert.AreEqual("CAR", printer.Printed[0]);
            Assert.AreEqual("CARD", printer.Printed[1]);
            Assert.AreEqual("CARDAN", printer.Printed[2]);
            
            Assert.AreEqual(3, printer.Printed.Count);
        }

        [Test]
        public void Should_return_number_of_words_when_find_for_prefix()
        {
            var input = new[] { "CAR", "CARD", "CARDAN", "TRY", "TRIES", "TRIED" };
            var printer = new FakePrinter("{0:#}");
            var tries = new Tries();
            foreach (var s in input)
            {
                tries.Add(s);
            }

            var cCount = tries.Find("C");
            Assert.AreEqual(3, cCount);
            var carCount = tries.Find("CAR");
            Assert.AreEqual(3, carCount);
            var cardCount = tries.Find("CARD");
            Assert.AreEqual(2, cardCount);
            var cardanCount = tries.Find("CARDAN");
            Assert.AreEqual(1, cardanCount);

            var tCount = tries.Find("T");
            Assert.AreEqual(3, tCount);
            var trCount = tries.Find("TR");
            Assert.AreEqual(3, trCount);
            var tryCount = tries.Find("TRY");
            Assert.AreEqual(1, tryCount);
            var trieCount = tries.Find("TRIE");
            Assert.AreEqual(2, trieCount);
            var triesCount = tries.Find("TRIES");
            Assert.AreEqual(1, triesCount);
            var triedCount = tries.Find("TRIED");
            Assert.AreEqual(1, triedCount);

            var nCount = tries.Find("N");
            Assert.AreEqual(0, nCount);
            
            tries.Print(printer);

            foreach (var s in input)
            {
                Assert.IsTrue(printer.Printed.Contains(s));
            }
            Assert.AreEqual(6, printer.Printed.Count);
        }
    }
}
