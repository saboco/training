using NUnit.Framework;
using Training.Tests.Common;

namespace Training.HackerRank.Tests.Algorithms
{
    public class CountingInversionTests
    {
        [TestCase(new[] {2, 1, 3, 1, 2}, ExpectedResult = 4)]
        [TestCase(new[] {1, 1, 1, 2, 2, 3}, ExpectedResult = 0)]
        public long Should_count_inversions(int[] arr)
        {
            var count = HackerRank.Algorithms.CountingInversions.CountInversions(arr);
            AssertHelpers.AssertIsSorted(arr);
            return count;
        }
    }
}