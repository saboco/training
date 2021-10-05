using System;

namespace Training.Common.Algorithms
{
    public class TreeHeight
    {
        public static int Height(BinaryNode root)
        {
            return Depth(root) - 1;
        }
        private static int Depth(BinaryNode node)
        {
            if (node == null)
            { return 0; }

            var lDepth = Depth(node.Left);
            var rDepth = Depth(node.Right);

            return Math.Max(lDepth, rDepth) + 1;
        }

        public static int Height(Node root)
        { 
            return Depth(root, 0);
        }
        private static int Depth(Node node, int current)
        {
            if(node==null)
            { 
                return 0; 
            }
            var depth=current;
            foreach(var n in node.Children)
            { 
                depth = Math.Max(depth, Depth(n, current + 1));
            }
            return depth;
        }
    }
}
