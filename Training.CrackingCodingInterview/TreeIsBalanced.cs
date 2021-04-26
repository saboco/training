using System;

namespace Training.CrackingCodingInterview
{
    public class TreeIsBalanced
    {
        public class Node
        {
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Data { get; }
            public Node(int data) { Data = data; }
        }

        private readonly Node _root;
        public TreeIsBalanced(Node root)
        { _root = root; }

        public bool IsBalanced()
        {
            var leftHeight = Height(_root.Left);
            var rightHeight = Height(_root.Right);
            return Math.Abs(leftHeight - rightHeight) <= 1;
        }

        private int Height(Node n)
        {
            if (n == null)
            { return 0; }
            var leftHeight = Height(n.Left) + 1;
            var rightHeight = Height(n.Right) + 1;
            return Math.Max(leftHeight, rightHeight);
        }

        public bool IsBalanced2()
        {
            var (leftWasBalanced, leftHeight) = Height2(_root.Left);
            var (rightWasBalanced, rightHeight) = Height2(_root.Right);
            return leftWasBalanced && rightWasBalanced && Math.Abs(leftHeight - rightHeight) <= 1;
        }

        private (bool, int) Height2(Node n)
        {
            if (n == null)
            { return (true, 0); }
            var (leftIsBalanced, leftHeight) = Height2(n.Left);
            var (rightIsBalanced, rightHeight) = Height2(n.Right);
            return (leftIsBalanced && rightIsBalanced && Math.Abs(leftHeight - rightHeight) <= 1, Math.Max(leftHeight, rightHeight) + 1);
        }

        private int MaxDepth(Node n)
        {
            if (n == null)
            { return 0; }
            return 1 + Math.Max(MaxDepth(n.Left), MaxDepth(n.Right));
        }

        private int MinDepth(Node n)
        {
            if (n == null)
            { return 0; }
            return 1 + Math.Min(MinDepth(n.Left), MinDepth(n.Right));
        }

        public bool IsBalanced3()
        {
            return MaxDepth(_root) - MinDepth(_root) <= 1;
        }
    }
}
