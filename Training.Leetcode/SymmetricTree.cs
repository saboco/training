using System;
using System.Collections.Generic;
using System.Text;

namespace Training.Leetcode
{
    public class SymmetricTree
    {
        public static bool IsSymmetric(TreeNode root)
        {
            return root == null
                ? true
                : Traverse(root.left, root.right);
        }

        private static bool Traverse(TreeNode left, TreeNode right)
        {
            if (left == null && right == null)
            {
                return true;
            }

            if (left == null || right == null)
            {
                return false;
            }

            return left.val == right.val
                && Traverse(left.left, right.right)
                && Traverse(left.right, right.left);
        }

        public static bool IsSymmetricIt(TreeNode root)
        {
            if (root == null)
                return true;
            var queue = new Queue<TreeNode>();
            if (root.left == null && root.right == null)
                return true;
            if (root.left == null || root.right == null)
                return false;

            queue.Enqueue(root.left);
            queue.Enqueue(root.right);
            while (queue.Count > 1)
            {
                var l = queue.Dequeue();
                var r = queue.Dequeue();
                if (l.val != r.val)
                {
                    return false;
                }
                if (l.left != null && r.right != null)
                {
                    queue.Enqueue(l.left);
                    queue.Enqueue(r.right);
                }
                else if (l.left == null && r.right != null
                    || l.left != null && r.right == null)
                {
                    return false;
                }
                if (l.right != null && r.left != null)
                {
                    queue.Enqueue(l.right);
                    queue.Enqueue(r.left);
                }
                else if (l.right != null && r.left == null
                   || l.right == null && r.left != null)
                {
                    return false;
                }
            }
            return queue.Count == 0;
        }

        public static bool IsSymmetricIt2(TreeNode root)
        {
            if (root == null)
            {
                return true;
            }

            var queue = new Queue<TreeNode>();
            queue.Enqueue(root.left);
            queue.Enqueue(root.right);
            while (queue.Count > 0)
            {
                var l = queue.Dequeue();
                var r = queue.Dequeue();
                if (l == null && r == null)
                {
                    continue;
                }
                if (l == null || r == null)
                {
                    return false;
                }
                if (l.val != r.val)
                {
                    return false;
                }

                queue.Enqueue(l.left);
                queue.Enqueue(r.right);
                queue.Enqueue(l.right);
                queue.Enqueue(r.left);
            }
            return queue.Count == 0;
        }

        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) { val = x; }
        }
    }
}
