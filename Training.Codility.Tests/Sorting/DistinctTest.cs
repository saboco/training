using NUnit.Framework;

namespace Training.Codility.Tests.Sorting
{
    public class DistinctTest
    {
        [TestCase(new[] { 2, 3, 1, 3, 3, 1, 2 }, ExpectedResult = 3)]
        [TestCase(new[] { 2, 3, 1, 3, 3, 1, 2, 4, 1, 9, 1, 10 }, ExpectedResult = 6)]
        [TestCase(new int[0], ExpectedResult = 0)]
        public int Should_retunr_the_number_of_distinct_values_in_a_array(int[] a)
        {
            return Codility.Sorting.Distinct.Solution.Solve(a);
        }
    }
}