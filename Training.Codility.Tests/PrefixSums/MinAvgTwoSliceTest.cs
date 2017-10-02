using NUnit.Framework;

namespace Training.Codility.Tests.PrefixSums
{
    public class MinAvgTwoSliceTest
    {
        [TestCase(new[] { 4, 2, 2, 5, 1, 5, 8 }, ExpectedResult = 1)]
        [TestCase(new[] { 2, 2, 4, 5, 1, 5, 8 }, ExpectedResult = 0)]
        [TestCase(new[] { 1, 1, 1, 1, 1, 1, 1 }, ExpectedResult = 0)]
        [TestCase(new[] { 1, 1 }, ExpectedResult = 0)]
        [TestCase(new[] { 1, 4 }, ExpectedResult = 0)]
        [TestCase(new[] { 1, 4, 2, 1 }, ExpectedResult = 2)]
        [TestCase(new[] { 1, 2, 3, 4 }, ExpectedResult = 0)]
        [TestCase(new[] { 10000, -10000 }, ExpectedResult = 0)]
        [TestCase(new[] { -3, -5, -8, -4, -10 }, ExpectedResult = 2, TestName = "All negatives")]
        public int Should_find_the_minimal_average_of_any_slice_containing_at_least_two_elements(int[] arr)
        {
            return Codility.PrefixSums.MinAvgTwoSlice.Solution.Solve(arr);
        }
    }
}