using System;
using System.Collections.Generic;

namespace Training.CrackingCodingInterview
{
    public class SearchBinaryTreeNodeWithParent
    {
        public class Node
        {
            public Node Parent { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Data { get; }
            public Node(int data)
            {
                Data = data;
            }
        }

        public static Node FromArray(int[] arr)
        {
            return FromArray(arr, null, 0, arr.Length - 1);
        }

        public static Node Search(int v, Node node)
        {
            if (v == node.Data)
            { return node; }

            if (v < node.Data)
            {
                return Search(v, node.Left);
            }
            else
            {
                return Search(v, node.Right);
            }
        }

        private static Node FromArray(int[] arr, Node parent, int lo, int hi)
        {
            if (lo > hi)
            { return null; }
            var mid = ((hi - lo) / 2) + lo;
            var n = new Node(arr[mid]);
            n.Parent = parent;
            n.Left = FromArray(arr, n, lo, mid - 1);
            n.Right = FromArray(arr, n, mid + 1, hi);
            return n;
        }

        public static Node Next(Node n)
        {
            Node root = n;
            while (root.Parent != null)
            {
                root = root.Parent;
            }
            var linkedList = new LinkedList<Node>();
            Next(root, n => linkedList.AddLast(n));
            var current = linkedList.First;
            while(current.Value.Data != n.Data)
            { 
                current=current.Next;
            }
            return current.Next?.Value;
        }
        private static void Next(Node n, Action<Node> f)
        {
            if (n == null)
            {
                return;
            }
            Next(n.Left, f);
            f(n);
            Next(n.Right, f);
        }
    }
}
