using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class SumPathsTests
    {
        [Theory]
        [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, 3, new[] { "3", "1¤2" })]
        [InlineData(new[] { 3, -4, 2, 1, 3, 2 }, 5, new[] { "2¤3", "2¤3", "3¤2" })]
        public void SumPathsTest(int[] arr, int target, string[] expectedPaths)
        {
            var bt = BinaryTree.FromArray(arr);
            var actualPaths = SumPaths.PrintSumPaths(bt.Root, target);
            Assert.Equal(expectedPaths.Length, actualPaths.Count);
            for (var i = 0; i < actualPaths.Count; i++)
            {
                Assert.Equal(expectedPaths[i], actualPaths[i]);
            }
        }
    }
}
