using System.Linq;
using NUnit.Framework;

namespace Training.Common.Tests
{
    public class ArrayHelpersTests
    {
        [TestCase(new[] {1, 2, 5, 6}, new[] {2, 5, 8}, ExpectedResult = new[] {2, 5})]
        public int[] Intersection_Should_find_the_intersaction_between_two_ordered_arrays(int[] arrA, int[] arrB)
        {
            return ArrayHelpers.Intersection(arrA, arrB).ToArray();
        }

        [TestCase(new[] {1, 2, 2, 4, 5}, new[] {1, 2, 2, 2, 6, 9, 10}, ExpectedResult = new[] {1, 2})]
        public int[] Intersection_Should_return_a_array_with_unique_values(int[] arrA, int[] arrB)
        {
            return ArrayHelpers.Intersection(arrA, arrB).ToArray();
        }

        [TestCase(null, null, ExpectedResult = new int[0])]
        [TestCase(new[] {1, 2}, null, ExpectedResult = new int[0])]
        [TestCase(null, new[] {1, 2}, ExpectedResult = new int[0])]
        public int[] Intersection_Should_handle_null_values_and_return_empty_array(int[] arrA, int[] arrB)
        {
            return ArrayHelpers.Intersection(arrA, arrB).ToArray();
        }

        [TestCase(new int[0], new int[0], ExpectedResult = new int[0])]
        [TestCase(new[] {1, 2}, new int[0], ExpectedResult = new int[0])]
        [TestCase(new int[0], new[] {1, 2}, ExpectedResult = new int[0])]
        public int[] Intersection_Should_handle_empty_arrays_and_return_empty_array(int[] arrA, int[] arrB)
        {
            return ArrayHelpers.Intersection(arrA, arrB).ToArray();
        }
    }
}