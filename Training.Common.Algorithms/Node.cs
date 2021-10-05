using System.Linq;
using System.Collections.Generic;

namespace Training.Common.Algorithms
{
    public class Node
    {
        public int Value { get; }
        public List<Node> Children { get; }
        public Node Parent { get; set; }
        public int Degree { get; set; }

        public Node(int v, Node[] children)
        { 
            Value = v;
            Children=children.ToList();
        }
    }

    public class BinaryNode
    {
        public int Value { get; }
        public BinaryNode Left { get; }
        public BinaryNode Right {get; }
        public BinaryNode(int v, BinaryNode left, BinaryNode right)
        { 
            Value = v;
            Left=left;
            Right=right;
        }
    }
}
