using System;

namespace Training.CrackingCodingInterview
{
    public class CommonAncestor
    {
        public class Node
        {
            public int Data { get; }
            public Node Parent { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(int data)
            {
                Data = data;
            }
        }

        public static Node GetCommonAncestor(Node n1, Node n2)
        {
            if (n1 == null || n2 == null)
            { return null; }

            int d1 = Depth(n1);
            int d2 = Depth(n2);
            return d1 > d2
                ? GetCommonAncestor(d1, n1, d2, n2)
                : GetCommonAncestor(d2, n2, d1, n1);
        }
        private static Node GetCommonAncestor(int deepest, Node n1, int shallow, Node n2)
        {
            while (deepest > shallow)
            {
                n1 = n1.Parent;
                if (n1 == n2)
                {
                    return n1.Parent;
                }
                deepest--;
            }
            while (n1 != n2)
            {
                n1 = n1.Parent;
                n2 = n2.Parent;
            }
            return n1;
        }

        private static int Depth(Node n)
        {
            var i = 0;
            while (n != null)
            {
                i++;
                n = n.Parent;
            }
            return i;
        }

        public static (Node, Node, Node) FromArray(int[] arr, int target1, int target2)
        {
            return FromArray(null, arr, 0, arr.Length - 1, target1, target2);
        }
        private static (Node, Node, Node) FromArray(Node parent, int[] arr, int lo, int hi, int target1, int target2)
        {
            if (lo > hi)
            { return (null, null, null); }
            var mid = lo + ((hi - lo) / 2);
            var n = new Node(arr[mid]);
            (Node left, Node ln1, Node ln2) = FromArray(n, arr, lo, mid - 1, target1, target2);
            n.Left = left;
            (Node right, Node rn1, Node rn2) = FromArray(n, arr, mid + 1, hi, target1, target2);
            n.Right = right;
            n.Parent = parent;
            Node node1 = ChooseTarget(ln1, rn1, n, target1);
            Node node2 = ChooseTarget(ln2, rn2, n, target2);

            return (n, node1, node2);
        }

        private static Node ChooseTarget(Node n1, Node n2, Node n, int target)
        {
            if (n.Data == target)
            {
                return n;
            }
            return n1 != null ? n1 : n2;
        }
    }
}
