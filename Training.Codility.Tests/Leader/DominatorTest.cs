using System.Linq;
using Xunit;

namespace Training.Codility.Tests.Leader
{
    public class DominatorTest
    {
        [Theory]
        [InlineData(new[] {3, 4, 3, 2, 3, -1, 3, 3}, true)]
        public void 
            Should_find_an_index_of_an_array_such_that_its_value_occurs_at_more_than_half_of_indices_in_the_array(
                int[] a, bool expected)
        {
            var result = Codility.Leader.Dominator.Solution.Solve(a);
            Assert.Equal(expected, new[] {0, 2, 4, 6, 7}.Contains(result));
        }
    }
}