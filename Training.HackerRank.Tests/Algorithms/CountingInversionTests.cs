using System.Linq;
using Xunit;
using Training.HackerRank.Algorithms;
using Training.Tests.Common;

namespace Training.HackerRank.Tests.Algorithms
{
    public class CountingInversionTests
    {
        [Theory]
        [InlineData(new[] { 2, 1, 3, 1, 2 }, 4)]
        [InlineData(new[] { 1, 1, 1, 2, 2, 3 }, 0)]
        public void Should_count_inversions(int[] arr, long expected)
        {
            var count = CountingInversions.CountInversions(arr);
            AssertHelpers.AssertIsSorted(arr);
            Assert.Equal(expected, count);
        }

        [Fact]
        public void Should_count_inversions_even_when_large_input()
        {
            var arr = Enumerable.Range(1, 65911).ToArray();
            var count = CountingInversions.CountInversions(arr);
            Assert.Equal(0, count);

            var arr2 = Enumerable.Range(1, 73099).ToArray();
            var count2 = CountingInversions.CountInversions(arr2);
            Assert.Equal(0, count2);

            var arr3 = Enumerable.Range(1, 32688).ToArray();
            var count3 = CountingInversions.CountInversions(arr3);
            Assert.Equal(0, count3);

            var arr4 = Enumerable.Range(1, 100000).Reverse().ToArray();
            var count4 = CountingInversions.CountInversions(arr4);
            Assert.Equal(4999950000, count4);

            var arr5 = Enumerable.Range(1, 100000).Reverse().ToArray();
            var count5 = CountingInversions.CountInversions(arr5);
            Assert.Equal(4999950000, count5);
        }
    }
}