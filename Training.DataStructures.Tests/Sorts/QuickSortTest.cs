using Xunit;
using Training.DataStructures.Sorting;
using Training.Tests.Common;

namespace Training.DataStructures.Tests.Sorts
{
    public class QuickSortTest
    {
        [Theory]
        [InlineData(new[] {5, 3, 4, 4, 8, 6, 7})]
        [InlineData(new[] {5, 3, 6, 4, 8, 4, 2})]
        [InlineData(new[] {15, 6, 1, 2, 3, 4, 9, 6, 4, 8, 0, 0})]
        [InlineData(new[] {0, 1, 2, 2, 2, 3, 4, 5, 8, 9})]
        [InlineData(new[] {15, 0, 1, 2, 2, 2, 3, 4, 5, 8, 0})]
        public void Should_sort_array(int[] arr)
        {
            var sortedArr = Quick.Sort(arr);
            AssertHelpers.AssertIsSorted(sortedArr);
        }
    }
}