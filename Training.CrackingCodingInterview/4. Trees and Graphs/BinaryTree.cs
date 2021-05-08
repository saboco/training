using System;
using System.Collections.Generic;

namespace Training.CrackingCodingInterview
{
    public class BinaryTree
    {
        public class Node
        {
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Data { get; }
            public Node(int data)
            {
                Data = data;
            }
        }

        public Node Root { get; private set; }

        public BinaryTree(Node root)
        {
            Root = root;
        }

        public void InOrderTraverse(Action<Node> f)
        {
            InOrderTraverse(Root, f);
        }

        private void InOrderTraverse(Node n, Action<Node> f)
        {
            if (n == null)
            { return; }
            InOrderTraverse(n.Left, f);
            f(n);
            InOrderTraverse(n.Right, f);
        }

        public void InOrderTraverseWithStack(Action<Node> f)
        {
            var current = Root;
            var s = new Stack<Node>();
            while (current != null || !s.Empty)
            {
                while (current != null)
                {
                    s.Push(current);
                    current = current.Left;
                }

                current = s.Pop();
                f(current);
                current = current.Right;
            }
        }

        public void PreOrderTraverse(Action<Node> f)
        {
            PreOrderTraverse(Root, f);
        }

        private void PreOrderTraverse(Node n, Action<Node> f)
        {
            if (n == null)
            { return; }
            f(n);
            PreOrderTraverse(n.Left, f);
            PreOrderTraverse(n.Right, f);
        }

        public void PreOrderTraverseWithStack(Action<Node> f)
        {
            if (Root == null)
            { return; }
            var current = Root;
            var s = new Stack<Node>();
            while (current != null || !s.Empty)
            {

                while (current != null)
                {
                    f(current);
                    if (current.Right != null)
                    {
                        s.Push(current.Right);
                    }
                    current = current.Left;
                }

                current = s.Pop();
            }
        }

        public void PreOrderTraverseIterative(Action<Node> f)
        {
            if (Root == null)
            { return; }
            var s1 = new Stack<Node>();
            s1.Push(Root);

            while (!s1.Empty)
            {
                var current = s1.Pop();
                f(current);
                if (current.Right != null)
                { s1.Push(current); }
                if (current.Left != null)
                { s1.Push(current); }

            }
        }

        public void PostOrderTraverse(Action<Node> f)
        {
            PostOrderTraverse(Root, f);
        }

        private void PostOrderTraverse(Node n, Action<Node> f)
        {
            if (n == null)
            { return; }
            PostOrderTraverse(n.Left, f);
            PostOrderTraverse(n.Right, f);
            f(n);
        }

        public void PostOrderTraverseWithTwoStacks(Action<Node> f)
        {
            if (Root == null)
            { return; }
            var s1 = new Stack<Node>();
            var s2 = new Stack<Node>();

            s1.Push(Root);

            while (!s1.Empty)
            {
                var current = s1.Pop();
                s2.Push(current);
                if (current.Left != null)
                { s1.Push(current); }
                if (current.Right != null)
                { s1.Push(current); }
            }

            while (!s2.Empty)
            {
                f(s2.Pop());
            }
        }

        public void PostOrderTraverseWithStack(Action<Node> f)
        {
            if (Root == null)
            { return; }
            var current = Root;
            var s = new Stack<Node>();
            while (current != null || !s.Empty)
            {
                while (current != null)
                {
                    if (current.Right != null)
                    { s.Push(current.Right); }
                    s.Push(current);
                    current = current.Left;
                }

                current = s.Pop();
                if (current.Right != null && !s.Empty && current.Right == s.Peek())
                {
                    s.Pop();
                    s.Push(current);
                    current = current.Right;
                }
                else
                {
                    f(current);
                    current = null;
                }
            }
        }

        public void LevelOrderTraverse(Action<Node> f)
        {
            if (Root == null)
            { return; }
            var queue = new Queue<Node>();
            queue.Enqueue(Root);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                f(current);
                if (current.Left != null)
                { queue.Enqueue(current.Left); }
                if (current.Right != null)
                { queue.Enqueue(current.Right); }
            }
        }

        public static BinaryTree FromArray(int[] arr)
        {
            return new BinaryTree(FromArray(arr, 0, arr.Length - 1));
        }

        private static Node FromArray(int[] arr, int lo, int hi)
        {
            if (lo > hi)
            { return null; }
            var mid = ((hi - lo) / 2) + lo;
            var n = new Node(arr[mid]);
            n.Left = FromArray(arr, lo, mid - 1);
            n.Right = FromArray(arr, mid + 1, hi);
            return n;
        }

        public int Height()
        {
            return Height(Root);
        }

        private int Height(Node n)
        {
            if (n == null)
            { return 0; }
            var leftHeight = Height(n.Left) + 1;
            var rightHeight = Height(n.Right) + 1;
            return Math.Max(leftHeight, rightHeight);
        }
    }

    public class BinarySearchTree : BinaryTree
    {
        public BinarySearchTree(Node root) : base(root) { }

        public Node Insert(int val)
        {
            return Insert(Root, val);
        }
        private Node Insert(Node root, int val)
        {
            if (root == null)
            {
                return new Node(val);
            }

            if (root.Data < val)
            {
                root.Right = Insert(root.Right, val);
            }
            else if (root.Data > val)
            {
                root.Left = Insert(root.Left, val);
            }
            return root;
        }

        public List<LinkedList<Node>> GetLevels()
        {
            if (Root == null)
            { return new List<LinkedList<Node>>(); }

            var queue = new Queue<(Node, int)>();
            queue.Enqueue((Root, 0));
            var visited = new HashSet<int>();
            visited.Add(Root.Data);
            var linkedLists = new List<LinkedList<Node>>();
            var currentLevel = -1;
            while (queue.Count > 0)
            {
                (Node current, int level) = queue.Dequeue();
                if (currentLevel != level)
                {
                    currentLevel = level;
                    linkedLists.Add(new LinkedList<Node>());
                }
                linkedLists[currentLevel].AddLast(current);
                if (current.Left != null)
                {
                    queue.Enqueue((current.Left, level + 1));
                }
                if (current.Right != null)
                {
                    queue.Enqueue((current.Right, level + 1));
                }
            }
            return linkedLists;
        }

        public static new BinarySearchTree FromArray(int[] arr)
        {
            var bt = BinaryTree.FromArray(arr);
            return new BinarySearchTree(bt.Root);
        }
    }
}
