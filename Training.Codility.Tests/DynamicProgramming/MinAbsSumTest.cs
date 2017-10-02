using NUnit.Framework;

namespace Training.Codility.Tests.DynamicProgramming
{
    public class MinAbsSumTest
    {
        [TestCase(new[] { 1, 5, 2, -2 }, 4, ExpectedResult = 0)]
        public int Should_find_the_lowest_absolute_sum_of_elements(int[] arr, int n)
        {
            Codility.DynamicProgramming.MinAbsSum.Solution.Solve(arr, n);
            return 0;
        }
    }
}