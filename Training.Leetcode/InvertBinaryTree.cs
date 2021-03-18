using System.Collections.Generic;

namespace Training.Leetcode
{
    public class InvertBinaryTree
    {
        public static TreeNode InvertTree(TreeNode root)
        {
            if (root == null)
            {
                return root;
            }

            var left = InvertTree(root.left);
            var right = InvertTree(root.right);
            root.right = left;
            root.left = right;
            return root;
        }

        public static TreeNode InvertTreeDFS(TreeNode root)
        {
            var stack = new Stack<TreeNode>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current != null)
                {
                    stack.Push(current.left);
                    stack.Push(current.right);

                    var tmp = current.right;
                    current.right = current.left;
                    current.left = tmp;
                }
            }

            return root;
        }

        public static TreeNode InvertTreeBFS(TreeNode root)
        {
            var queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current != null)
                {
                    queue.Enqueue(current.left);
                    queue.Enqueue(current.right);

                    var tmp = current.right;
                    current.right = current.left;
                    current.left = tmp;
                }
            }

            return root;
        }

        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) => val = x;
        }
    }
}
