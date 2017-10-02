using NUnit.Framework;
using Training.Codility.TimeComplexity.PermMissingElem;

namespace Training.Codility.Tests.TimeComplexity
{
    public class PermMissingElemTest
    {
        [TestCase(new[] { 2, 3, 1, 5 },ExpectedResult = 4)]
        [TestCase(new[] { 4, 3, 1, 5 }, ExpectedResult = 2)]
        public int Should_return_the_missing_elem_in_permutation(int[] arr)
        {
            return Solution.Solve(arr);
        }
    }
}