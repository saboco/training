using Xunit;
using Training.HackerRank.DataStructures;
using Training.Tests.Common;

namespace Training.HackerRank.Tests.DataStructures
{    
    public class ATaleOfTwoStacksTests
    {
        [Fact]
        public void Should_produces_the_expected_output_when_given_transactions()
        {
            var input = new[] {"10", "1 42", "2", "1 14", "3", "1 28", "3", "1 60", "1 78", "2", "2"};

            var isFirst = true;
            var printer = new FakePrinter("{0:#}");
            var sut = new ATaleOfTwoStacks(printer);
            foreach (var transaction in input)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sut.ParseTrasaction(transaction);
                }
            }

            var output = new[] {"14", "14"};
            var i = 0;
            foreach (var s in output)
            {
                Assert.Equal(s, printer.Printed[i++].ToString());
            }
        }
    }
}