using System.Linq;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class BinarySearchTest
    {
        [Fact]
        public void Should_search_for_a_number_in_a_sorted_array()
        {
            var arr = new[] { 1, 5, 6, 9, 10, 12, 100, 120, 150, 4215 };
            var n = BinarySearch.Search(arr, 9);
            Assert.Equal(3, n);
        }

        [Fact]
        public void Should_search_for_a_number_in_a_sorted_array_even_when_huge()
        {
            var arr = Enumerable.Range(1, 10000000).ToArray();
            var n = BinarySearch.Search(arr, 72);
            Assert.Equal(71, n);
        }

        [Theory]
        [InlineData(new[] { -100, -40, -20, -8, -5, 5, 6, 9, 10, 12, 100, 120, 150, 4215 }, 4)]
        [InlineData(new[] { -8, -1, 5, 6, 9, 10 }, 1)]
        [InlineData(new[] { -25, -8, -1, 5, 6, 9, 10 }, 2)]
        public void Should_search_for_a_number_or_nearest_in_a_sorted_array(int[] arr, int expected)
        {
            Assert.Equal(expected, BinarySearch.SearchNearest(arr, 0));
        }
    }
}