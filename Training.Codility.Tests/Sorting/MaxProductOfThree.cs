using Xunit;

namespace Training.Codility.Tests.Sorting
{
    public class MaxProductOfThree
    {
        [Theory]
        [InlineData(new[] { -3, 1, 2, -2, 5, 6 }, 60)]
        [InlineData(new[]{ -5, 5, -5, 4}, 125)]
        [InlineData(new[] { -4, -6, 3, 4, 5 }, 120)]
        public void Should_maximize_the_product_of_triples_in_a_array(int[] a, int expected)
        {
            Assert.Equal(expected, Codility.Sorting.MaxProductOfThree.Solution.Solve(a));
        }
    }
}