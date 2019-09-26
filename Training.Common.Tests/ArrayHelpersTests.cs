using System.Linq;
using Xunit;

namespace Training.Common.Tests
{
    public class ArrayHelpersTests
    {
        [Theory]
        [InlineData(new[] { 1, 2, 5, 6 }, new[] { 2, 5, 8 }, new[] { 2, 5 })]
        public void Intersection_Should_find_the_intersaction_between_two_ordered_arrays(int[] arrA, int[] arrB, int[] expected)
        {
            Assert.Equal(expected, ArrayHelpers.Intersection(arrA, arrB).ToArray());
        }

        [Theory]
        [InlineData(new[] { 1, 2, 2, 4, 5 }, new[] { 1, 2, 2, 2, 6, 9, 10 }, new[] { 1, 2 })]
        public void Intersection_Should_return_a_array_with_unique_values(int[] arrA, int[] arrB, int[] expected)
        {
            Assert.Equal(expected, ArrayHelpers.Intersection(arrA, arrB).ToArray());
        }

        [Theory]
        [InlineData(null, null, new int[0])]
        [InlineData(new[] { 1, 2 }, null, new int[0])]
        [InlineData(null, new[] { 1, 2 }, new int[0])]
        public void Intersection_Should_handle_null_values_and_return_empty_array(int[] arrA, int[] arrB, int[] expected)
        {
            Assert.Equal(expected, ArrayHelpers.Intersection(arrA, arrB).ToArray());
        }

        [Theory]
        [InlineData(new int[0], new int[0], new int[0])]
        [InlineData(new[] { 1, 2 }, new int[0], new int[0])]
        [InlineData(new int[0], new[] { 1, 2 }, new int[0])]
        public void Intersection_Should_handle_empty_arrays_and_return_empty_array(int[] arrA, int[] arrB, int[] expected)
        {
            Assert.Equal(expected, ArrayHelpers.Intersection(arrA, arrB).ToArray());
        }
    }
}