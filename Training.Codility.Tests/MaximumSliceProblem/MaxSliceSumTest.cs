using NUnit.Framework;

namespace Training.Codility.Tests.MaximumSliceProblem
{
    public class MaxSliceSumTest
    {
        [TestCase(new[] {3, 2, -6, 4, 0}, ExpectedResult = 5)]
        [TestCase(new[] {-10}, ExpectedResult = -10)]
        [TestCase(new int[0], ExpectedResult = 0)]
        public int Should_return_the_maximum_sum_of_a_slice(int[] a)
        {
            return Codility.MaximumSliceProblem.MaxSliceSum.Solution.Solve(a);
        }
    }
}