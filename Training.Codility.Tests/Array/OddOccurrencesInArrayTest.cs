using NUnit.Framework;
using Training.Codility.Array.OddOccurrencesInArray;

namespace Training.Codility.Tests.Array
{
    public class OddOccurrencesInArray
    {
        [TestCase(new[] { 9, 3, 9, 3, 9, 7, 9 }, ExpectedResult = 7)]
        [TestCase(new[] { 9, 3, 9, 3, 9, 10, 9 }, ExpectedResult = 10)]
        [TestCase(new[] { 9, 3, 9, 3, 9, 1, 9 }, ExpectedResult = 1)]
        public int Should_return_the_odd_in_array(int[] arr)
        {
            return Solution.Solve(arr);
        }
    }
}