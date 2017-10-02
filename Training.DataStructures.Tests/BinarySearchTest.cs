using System.Linq;
using NUnit.Framework;

namespace Training.DataStructures.Tests
{
    public class BinarySearchTest
    {
        [Test]
        public void Should_search_for_a_number_in_a_sorted_array()
        {
            var arr = new[] { 1, 5, 6, 9, 10, 12, 100, 120, 150, 4215 };
            var n = BinarySearch.Search(arr, 9);
            Assert.AreEqual(3, n);
        }

        [Test]
        public void Should_search_for_a_number_in_a_sorted_array_even_when_huge()
        {
            var arr = Enumerable.Range(1, 10000000).ToArray();
            var n = BinarySearch.Search(arr, 72);
            Assert.AreEqual(71, n);
        }

        [TestCase(new[] { -100, -40, -20, -8, -5, 5, 6, 9, 10, 12, 100, 120, 150, 4215 }, ExpectedResult = 4)]
        [TestCase(new[] { -8, -1, 5, 6, 9, 10 }, ExpectedResult = 1)]
        [TestCase(new[] { -25, -8, -1, 5, 6, 9, 10 }, ExpectedResult = 2)]
        public int Should_search_for_a_number_or_nearest_in_a_sorted_array(int[] arr)
        {
            return BinarySearch.SearchNearest(arr, 0);
        }
    }
}