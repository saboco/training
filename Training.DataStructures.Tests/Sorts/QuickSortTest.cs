using NUnit.Framework;
using Training.DataStructures.Sorting;
using Training.Tests.Common;

namespace Training.DataStructures.Tests.Sorts
{
    public class QuickSortTest
    {
        [TestCase(new[] {5, 3, 4, 4, 8, 6, 7})]
        [TestCase(new[] {5, 3, 6, 4, 8, 4, 2})]
        [TestCase(new[] {15, 6, 1, 2, 3, 4, 9, 6, 4, 8, 0, 0})]
        [TestCase(new[] {0, 1, 2, 2, 2, 3, 4, 5, 8, 9})]
        [TestCase(new[] {15, 0, 1, 2, 2, 2, 3, 4, 5, 8, 0})]
        public void Should_sort_array(int[] arr)
        {
            var sortedArr = Quick.Sort(arr);
            AssertHelpers.AssertIsSorted(sortedArr);
        }
    }
}