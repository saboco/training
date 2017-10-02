using System.Linq;
using NUnit.Framework;
using Training.Codility.Sorting.NumberOfDiscIntersections;

namespace Training.Codility.Tests.Sorting
{
    public class NumberOfDiscIntersectionsTest
    {
        [TestCase(new[] {1, 5, 2, 1, 4, 0}, ExpectedResult = 11)]
        [TestCase(new[]{1, 2147483647, 0}, ExpectedResult = 2)]
        public int Should_compute_the_number_of_intersections_in_a_sequence_of_discs(int[] a)
        {
            return Solution.Solve(a.Select(i => (long)i).ToArray());
        }
    }
}