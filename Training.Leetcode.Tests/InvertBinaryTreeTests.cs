using System;
using Xunit;
using TreeNode = Training.Leetcode.InvertBinaryTree.TreeNode;

namespace Training.Leetcode.Tests
{
    public class InvertBinaryTreeTests
    {
        private readonly Func<TreeNode, TreeNode>[] _actions =
        {
            InvertBinaryTree.InvertTree,
            InvertBinaryTree.InvertTreeBFS,
            InvertBinaryTree.InvertTreeDFS
        };

        [Theory]
        [InlineData(new[] { 4, 7, 2, 9, 6, 3, 1 }, new[] { 4, 2, 7, 1, 3, 6, 9 })]
        [InlineData(new[] { 4, 7, 2, 9, -1, 3, 1 }, new[] { 4, 2, 7, 1, 3, -1, 9 })]
        public void InvertBinaryTreeTest(int[] expected, int[] nodes)
        {
            foreach (var action in _actions)
            {
                TreeNode root = null;
                if (nodes.Length > 0)
                {
                    root = BuildTree(0, nodes);
                }
                CheckTree(action.Invoke(root), 0, expected);
            }
        }

        private void CheckTree(TreeNode treeNode, int current, int[] expected)
        {
            if (current > expected.Length - 1)
            {
                return;
            }
            if (treeNode == null)
            {
                Assert.Equal(expected[current], -1);
                return;
            }
            else
            {
                Assert.Equal(expected[current], treeNode.val);
            }
            CheckTree(treeNode.left, current * 2 + 1, expected);
            CheckTree(treeNode.right, current * 2 + 2, expected);
        }

        private TreeNode BuildTree(int current, int[] nodes)
        {
            if (current > nodes.Length - 1)
            { return null; }

            if (nodes[current] == -1)
            { return null; }

            var root = new TreeNode(nodes[current])
            {
                left = BuildTree(current * 2 + 1, nodes),
                right = BuildTree(current * 2 + 2, nodes)
            };
            return root;
        }
    }
}
