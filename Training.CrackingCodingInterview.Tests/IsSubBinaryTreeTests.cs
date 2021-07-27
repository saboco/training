using System;
using System.Collections.Generic;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class IsSubBinaryTreeTests
    {
        List<Func<BinaryTree.Node, BinaryTree.Node, bool>> _actions = new List<Func<BinaryTree.Node, BinaryTree.Node, bool>>
        {
            BinaryTree.IsSubTree,
            //BinaryTree.IsSubTreeRec
        };
        [Theory]
        [InlineData(new[] { 7 }, new[] { 7 }, true)]
        [InlineData(new[] { 7 }, new[] { 10 }, false)]
        [InlineData(new int[0], new int[0], true)]
        [InlineData(new[] { 4 }, new int[0], true)]
        [InlineData(new int[0], new[] { 4 }, false)]
        [InlineData(new[] { 7, 8, 1, 4, 5, 2, 9, 6, 1, 10, 12 }, new[] { 10 }, true)]
        [InlineData(new[] { 7, 8, 1, 4, 5, 2, 9, 6, 1, 10, 12 }, new[] { 10, 12 }, true)]
        [InlineData(new[] { 7, 8, 1, 4, 5, 2, 9, 6, 1, 10, 12 }, new[] { 9, 6, 1, 10, 12 }, true)]
        [InlineData(new[] { 7, 8, 1, 4, 5, 2, 9, 6, 1, 10, 12 }, new[] { 7, 8 }, true)]
        [InlineData(new[] { 7, 8, 1, 4, 5, 2, 9, 6, 1, 10, 12 }, new[] { 7, 8, 1, 4 }, false)]
        public void IsSubTreeTest(int[] t1arr, int[] t2arr, bool expected)
        {
            foreach (var action in _actions)
            {
                var t1 = BinaryTree.FromArray(t1arr);
                var t2 = BinaryTree.FromArray(t2arr);
                var actual = action.Invoke(t1.Root, t2.Root);
                Assert.Equal(expected, actual);
            }
        }
    }
}
