using NUnit.Framework;

namespace Training.Codility.Tests.Sorting
{
    public class TriangleTest
    {
        [TestCase(new[] { 10, 2, 5, 1, 8, 20 }, ExpectedResult = 1)]
        [TestCase(new[] { 10, 50, 5, 1 }, ExpectedResult = 0)]
        [TestCase(new[] { 2147248346, 247483647, 2147248345 }, ExpectedResult = 1)]
        public int Should_determine_if_a_triangle_can_be_build(int[] a)
        {
            return Codility.Sorting.Triangle.Solution.Solve(a);
        }
    }
}