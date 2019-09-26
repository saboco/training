using Xunit;
using Training.Tests.Common;

namespace Training.DataStructures.Tests
{
    
    public class TriesTest
    {
        [Fact]
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
            Assert.Equal("CAR", printer.Printed[0]);
            Assert.Equal("CARD", printer.Printed[1]);
            Assert.Equal("CARDAN", printer.Printed[2]);
            
            Assert.Equal(3, printer.Printed.Count);
        }

        [Fact]
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
            Assert.Equal(3, cCount);
            var carCount = tries.Find("CAR");
            Assert.Equal(3, carCount);
            var cardCount = tries.Find("CARD");
            Assert.Equal(2, cardCount);
            var cardanCount = tries.Find("CARDAN");
            Assert.Equal(1, cardanCount);

            var tCount = tries.Find("T");
            Assert.Equal(3, tCount);
            var trCount = tries.Find("TR");
            Assert.Equal(3, trCount);
            var tryCount = tries.Find("TRY");
            Assert.Equal(1, tryCount);
            var trieCount = tries.Find("TRIE");
            Assert.Equal(2, trieCount);
            var triesCount = tries.Find("TRIES");
            Assert.Equal(1, triesCount);
            var triedCount = tries.Find("TRIED");
            Assert.Equal(1, triedCount);

            var nCount = tries.Find("N");
            Assert.Equal(0, nCount);
            
            tries.Print(printer);

            foreach (var s in input)
            {
                Assert.Contains(s, printer.Printed);
            }
            Assert.Equal(6, printer.Printed.Count);
        }
    }
}
