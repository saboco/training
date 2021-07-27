using System;
using System.Collections.Generic;
using Xunit;
using TreeNode = Training.Leetcode.ConvertSortedArrayToBinarySearchTree.TreeNode;
namespace Training.Leetcode.Tests
{
    public class ConvertSortedArrayToBinarySearchTreeTests
    {
        private readonly Func<int[], TreeNode>[] _actions =
        {
            ConvertSortedArrayToBinarySearchTree.SortedArrayToBST,
            ConvertSortedArrayToBinarySearchTree.SortedArrayToBST2,
            ConvertSortedArrayToBinarySearchTree.SortedArrayToBST3,
            ConvertSortedArrayToBinarySearchTree.SortedArrayToBST4,
            ConvertSortedArrayToBinarySearchTree.SortedArrayToBST5
        };

        [Theory]
        [InlineData(new[] { -10, -3, 0, 5, 9 })]
        [InlineData(new[] { 0, 1, 2, 3, 4, 5 })]
        [InlineData(new int[0])]
        // apparently duplicate keys are not allowed in BST [InlineData(new[] { 1, 2, 2, 2, 2 })]
        public void ConvertSortedArrayToBinarySearchTreeTest(int[] nums)
        {
            foreach (var action in _actions)
            {
                var root = action.Invoke(nums);
                if (root == null)
                {
                    Assert.Empty(nums);
                    return;
                }
                var leftHeight = 0;
                var rightHeight = 0;

                if (root.left != null)
                {
                    leftHeight = DFS(root.left);
                }
                if (root.right != null)
                {
                    rightHeight = DFS(root.right);
                }
                Assert.True(Math.Abs(leftHeight - rightHeight) <= 1);
                var actualNums = new List<int>();
                TraverseInOrder(root, actualNums);
                var previous = int.MinValue;
                var i = 0;
                foreach (var num in actualNums)
                {
                    Assert.Equal(nums[i], num);
                    Assert.True(previous <= num);
                    previous = num;
                    i++;
                }
            }
        }
        private int DFS(TreeNode current)
        {
            if (current == null)
            {
                return 0;
            }

            var left = DFS(current.left) + 1;
            var right = DFS(current.right) + 1;
            return Math.Max(left, right);
        }

        private void TraverseInOrder(TreeNode root, List<int> nums)
        {
            if (root == null)
            {
                return;
            }

            TraverseInOrder(root.left, nums);
            nums.Add(root.val);
            TraverseInOrder(root.right, nums);
        }
    }
}
