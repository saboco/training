using NUnit.Framework;

namespace Training.Codility.Tests.MaximumSliceProblem
{
    public class MaxProfitTest
    {
        [TestCase(new[] {23171, 21011, 21123, 21366, 21013, 21367}, ExpectedResult = 356)]
        [TestCase(new[] {23171, 21011, 21123, 21367, 21013, 21366}, ExpectedResult = 356)]
        [TestCase(new[] {21011, 23171, 21123, 21367, 21013, 21366}, ExpectedResult = 2160)]
        public int Should_return_the_max_profit(int[] a)
        {
            return Codility.MaximumSliceProblem.MaxProfit.Solution.Solve(a);
        }
    }
}