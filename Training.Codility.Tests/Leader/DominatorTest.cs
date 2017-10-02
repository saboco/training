using System.Linq;
using NUnit.Framework;

namespace Training.Codility.Tests.Leader
{
    public class DominatorTest
    {
        [TestCase(new[] {3, 4, 3, 2, 3, -1, 3, 3}, ExpectedResult = true)]
        public bool
            Should_find_an_index_of_an_array_such_that_its_value_occurs_at_more_than_half_of_indices_in_the_array(
                int[] a)
        {
            var result = Codility.Leader.Dominator.Solution.Solve(a);
            return new[] {0, 2, 4, 6, 7}.Contains(result);
        }
    }
}