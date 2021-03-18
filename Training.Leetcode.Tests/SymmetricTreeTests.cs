using System;
using Xunit;

namespace Training.Leetcode.Tests
{
    public class SymmetricTreeTests
    {
        readonly Func<SymmetricTree.TreeNode, bool>[] _actions =
        {
            SymmetricTree.IsSymmetric,
            SymmetricTree.IsSymmetricIt,
            SymmetricTree.IsSymmetricIt2
        };

        [Theory]
        [InlineData(true, new[] { 1, 2, 2, 3, 4, 4, 3 })]
        [InlineData(false, new[] { 1, 2, 2, -1, 3, -1, 3 })]
        [InlineData(false, new[] { 1, 2, 3 })]
        [InlineData(true, new [] { 1 })]
        [InlineData(false, new [] { 1, 1, 1, 1 })]
        [InlineData(false, new [] { 1, 1, 1, 1, 1 })]
        [InlineData(true, new int[0])]
        public void SymmetricTreeTest(bool expected, int[] tree)
        {
            foreach (var action in _actions)
            {
                SymmetricTree.TreeNode root = null;
                if (tree.Length > 0)
                {
                    root = Tree(0, tree);
                }
                Assert.Equal(expected, action.Invoke(root));
            }
        }

        private SymmetricTree.TreeNode Tree(int current, int[] tree)
        {
            if (current > tree.Length - 1)
            {
                return null;
            }
            if (tree[current] == -1)
            {
                return null;
            }

            var root = new SymmetricTree.TreeNode(tree[current]);
            root.left = Tree(2 * current + 1, tree);
            root.right = Tree(2 * current + 2, tree);

            return root;
        }
    }
}
