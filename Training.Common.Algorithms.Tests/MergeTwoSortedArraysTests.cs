using NUnit.Framework;

namespace Training.Common.Algorithms.Tests
{
    public class MergeTwoSortedArraysTests
    {
        [TestCase(new[] {1, 3, 8}, new[] {2, 4, 9, 10}, ExpectedResult = new[] {1, 2, 3, 4, 8, 9, 10},
            TestName = "Simple")]
        [TestCase(new int[0], new[] {2, 4, 9, 10}, ExpectedResult = new[] {2, 4, 9, 10}, TestName = "One Empty")]
        [TestCase(new int[0], new int[0], ExpectedResult = new int[0], TestName = "Two empty")]
        [TestCase(new[] {3}, new[] {2, 4, 9, 10}, ExpectedResult = new[] {2, 3, 4, 9, 10}, TestName = "Diferent sizes")]
        [TestCase(new[] {3}, new[] {2}, ExpectedResult = new[] {2, 3}, TestName = "Single element arrays")]
        [TestCase(new[] {3, 3, 3, 3, 3}, new[] {3, 3, 3, 3, 3}, ExpectedResult = new[] {3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
            TestName = "arrays of 3")]
        public int[] Should_merge_two_sorted_arrays_and_return_a_sorted_array(int[] arr1, int[] arr2)
        {
            return MergeTwoSortedArrays.MergeInOrder(arr1, arr2);
        }
    }
}