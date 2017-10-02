using NUnit.Framework;

namespace Training.Codility.Tests.MaximumSliceProblem
{
    public class MaxDoubleSliceSumTest
    {
        [TestCase(new[] {3, 2, 6, -1, 4, 5, -1, 2}, ExpectedResult = 17)]
        [TestCase(new[] {-8, 10, 20, -5, -7, -4}, ExpectedResult = 30)]
        public int Should_return_the_max_sum_of_any_double_slice(int[] a)
        {
            return Codility.MaximumSliceProblem.MaxDoubleSliceSum.Solution.Solve(a);
        }
    }
}