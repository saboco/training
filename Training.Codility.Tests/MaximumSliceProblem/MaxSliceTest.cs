using NUnit.Framework;

namespace Training.Codility.Tests.MaximumSliceProblem
{
    public class MaxSliceTest
    {
        [TestCase(new[] {5, -7, 3, 5, -2, 4, -1}, ExpectedResult = 10)]
        [TestCase(new[] {-5, -7, -3, -5, -2, -4, -1}, ExpectedResult = 0)]
        public int Should_return_the_max_slice(int[] a)
        {
            return Codility.MaximumSliceProblem.GoldenMaxSlice.Solve(a);
        }

        [TestCase(new[] {5, -7, 3, 5, -2, 4, -1}, ExpectedResult = 10)]
        [TestCase(new[] {-5, -7, -3, -5, -2, -4, -1}, ExpectedResult = 0)]
        public int Should_slowly_return_the_max_slice(int[] a)
        {
            return Codility.MaximumSliceProblem.SlowMaximalSlice.Solve(a);
        }
    }
}