using System.Collections.Generic;

namespace Training.Common.Algorithms
{
    public class TreeIsomorphism
    {
        public static bool AreIsomorphic(int[][] tree1, int[][] tree2)
        {
            var tree1Centers = TreeCenter.FindCenterWithoutHelperStructure(tree1);
            var rootedTree1 = TreeRooting.CanonicRootTree(tree1, tree1Centers[0]);
            var encodedTree1 = EncodeTree(rootedTree1);
             foreach (var center in TreeCenter.FindCenterWithoutHelperStructure(tree2))
            {
                var rootedTree2 = TreeRooting.CanonicRootTree(tree2, center);
                var encodedTree2 = EncodeTree(rootedTree2);
                if (encodedTree1 == encodedTree2)
                {
                    return true;
                }
            }
            return false;
        }

        private static string EncodeTree(Node node)
        {
            if(node.Children.Count==0)
            { 
                return "()";
            }
            var encodedChildren = new List<string>();
            foreach(var child in node.Children)
            { 
                encodedChildren.Add(EncodeTree(child));
            }
            encodedChildren.Sort();
            return $"({string.Join("", encodedChildren)})";
        }
    }
}
