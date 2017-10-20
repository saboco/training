using System.Linq;
using NUnit.Framework;
using Training.HackerRank.Algorithms;
using Training.Tests.Common;

namespace Training.HackerRank.Tests.Algorithms
{
    public class CountingInversionTests
    {
        [TestCase(new[] {2, 1, 3, 1, 2}, ExpectedResult = 4)]
        [TestCase(new[] {1, 1, 1, 2, 2, 3}, ExpectedResult = 0)]
        public long Should_count_inversions(int[] arr)
        {
            var count = CountingInversions.CountInversions(arr);
            AssertHelpers.AssertIsSorted(arr);
            return count;
        }

        [Test]
        public void Should_count_inversions_even_when_large_input()
        {
            //int a = 704982704;
            //long a = 4999950000;
            var arr = Enumerable.Range(1, 65911).ToArray();
            var count = CountingInversions.CountInversions(arr);
            Assert.AreEqual(0, count);

            var arr2 = Enumerable.Range(1, 73099).ToArray();
            var count2 = CountingInversions.CountInversions(arr2);
            Assert.AreEqual(0, count2);

            var arr3 = Enumerable.Range(1, 32688).ToArray();
            var count3 = CountingInversions.CountInversions(arr3);
            Assert.AreEqual(0, count3);

            var arr4 = Enumerable.Range(1, 100000).Reverse().ToArray();
            var count4 = CountingInversions.CountInversions(arr4);
            Assert.AreEqual(4999950000, count4);

            var arr5 = Enumerable.Range(1, 100000).Reverse().ToArray();
            var count5 = CountingInversions.CountInversions(arr5);
            Assert.AreEqual(4999950000, count5);
        }
    }
}