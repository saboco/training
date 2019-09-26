using System.Linq;
using Xunit;
using Training.Codility.Sorting.NumberOfDiscIntersections;

namespace Training.Codility.Tests.Sorting
{
    public class NumberOfDiscIntersectionsTest
    {
        [Theory]
        [InlineData(new[] { 1, 5, 2, 1, 4, 0 }, 11)]
        [InlineData(new[] { 1, 2147483647, 0 }, 2)]
        public void Should_compute_the_number_of_intersections_in_a_sequence_of_discs(int[] a, int expected)
        {
            Assert.Equal(expected, Solution.Solve(a.Select(i => (long)i).ToArray()));
        }
    }
}