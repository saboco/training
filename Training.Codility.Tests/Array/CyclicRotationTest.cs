using NUnit.Framework;
using Training.Codility.Array.CyclicRotation;

namespace Training.Codility.Tests.Array
{
    public class CyclicRotation
    {
        [TestCase(new[] { 3, 8, 9, 7, 6 }, 1, ExpectedResult = new[] { 6, 3, 8, 9, 7 })]
        [TestCase(new[] { 3, 8, 9, 7, 6 }, 2, ExpectedResult = new[] { 7, 6, 3, 8, 9 })]
        [TestCase(new[] { 3, 8, 9, 7, 6 }, 3, ExpectedResult = new[] { 9, 7, 6, 3, 8 })]
        [TestCase(new[] { 3, 8, 9, 7, 6 }, 4, ExpectedResult = new[] { 8, 9, 7, 6, 3 })]
        public int[] Should_rotate_the_array(int[] arr, int k)
        {
            return Solution.Solve(arr, k);
        }
    }
}