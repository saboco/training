using System.Collections.Generic;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class TreeIsomorphismTests
    {
        [Theory]
        [InlineData(6,
            true,
            //tree1
            new[] { 1 },
            new[] { 0, 2 },
            new[] { 1 },
            new[] { 4, 5 },
            new[] { 1, 3 },
            new[] { 3 },
            //tree2
            new[] { 1 },
            new[] { 0, 2 },
            new[] { 1, 4 },
            new[] { 4 },
            new[] { 2, 3, 5 },
            new[] { 4 }
            )]
        public void AreIsomorphicTest(int n, bool expected, params int[][] trees)
        {
            var tree1 = new List<int[]>();
            for (var i = 0; i < n; i++)
            {
                tree1.Add(trees[i]);
            }
            var tree2 = new List<int[]>();
            for (var i = n; i < n * 2; i++)
            {
                tree2.Add(trees[i]);
            }
            var actual = TreeIsomorphism.AreIsomorphic(tree1.ToArray(), tree2.ToArray());
            Assert.Equal(expected, actual);
        }
    }
}
