using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class MergeTwoSortedArraysTests
    {
        [Theory]
        [InlineData(new[] { 1, 3, 8 }, new[] { 2, 4, 9, 10 }, new[] { 1, 2, 3, 4, 8, 9, 10 })] //, TestName = "Simple"
        [InlineData(new int[0], new[] { 2, 4, 9, 10 }, new[] { 2, 4, 9, 10 })] //, TestName = "One Empty"
        [InlineData(new int[0], new int[0], new int[0])] //, TestName = "Two empty"
        [InlineData(new[] { 3 }, new[] { 2, 4, 9, 10 }, new[] { 2, 3, 4, 9, 10 })]//, TestName = "Diferent sizes"
        [InlineData(new[] { 3 }, new[] { 2 }, new[] { 2, 3 })]//, TestName = "Single element arrays"
        [InlineData(new[] { 3, 3, 3, 3, 3 }, new[] { 3, 3, 3, 3, 3 }, new[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 })] //, TestName = "arrays of 3"
        public void Should_merge_two_sorted_arrays_and_return_a_sorted_array(int[] arr1, int[] arr2, int[] expected)
        {
            Assert.Equal(expected, MergeTwoSortedArrays.MergeInOrder(arr1, arr2));
        }
    }
}