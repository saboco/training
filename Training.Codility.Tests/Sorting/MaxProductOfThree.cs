using NUnit.Framework;

namespace Training.Codility.Tests.Sorting
{
    public class MaxProductOfThree
    {
        [TestCase(new[] { -3, 1, 2, -2, 5, 6 }, ExpectedResult = 60)]
        [TestCase(new[]{ -5, 5, -5, 4}, ExpectedResult = 125)]
        [TestCase(new[] { -4, -6, 3, 4, 5 }, ExpectedResult = 120)]
        public int Should_maximize_the_product_of_triples_in_a_array(int[] a)
        {
            return Codility.Sorting.MaxProductOfThree.Solution.Solve(a);
        }
    }
}