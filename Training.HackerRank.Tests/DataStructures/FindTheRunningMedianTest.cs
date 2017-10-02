using NUnit.Framework;
using Training.HackerRank.DataStructures;
using Training.Tests.Common;

namespace Training.HackerRank.Tests.DataStructures
{
    [TestFixture]
    public class FindTheRunningMedianTest
    {
        [Test]
        public void Should_calculate_running_median_when_adding_integers()
        {
            var printer = new FakePrinter("{0:0.0}");
            var sut = new FindTheRunningMedian(printer);
            var input = new[] { 12, 4, 5, 3, 8, 7 };
            var output = new[] { "12.0", "8.0", "5.0", "4.5", "5.0", "6.0" };

            foreach (var i in input)
            {
                sut.Add(i);
            }

            var j = 0;
            foreach (var s in output)
            {
                Assert.AreEqual(s, printer.Printed[j++]);
            }
        }
    }
}
