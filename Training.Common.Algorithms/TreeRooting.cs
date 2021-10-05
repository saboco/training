using System;

namespace Training.Common.Algorithms
{
    public class TreeRooting
    {
        public static Node RootTree(int[][] tree, int root)
        {
            var visited = new bool[tree.Length];
            var rootedTree = new Node(root, Array.Empty<Node>());
            Dfs(tree, root, visited, rootedTree);
            return rootedTree;
        }

        private static void Dfs(int[][] tree, int node, bool[] visited, Node current)
        {
            visited[node] = true;
            foreach (var n in tree[node])
            {
                if (!visited[n])
                {
                    visited[n] = true;
                    var next = new Node(n, Array.Empty<Node>());
                    next.Parent = current;
                    current.Children.Add(next);
                    Dfs(tree, n, visited, next);
                }
            }
        }

        public static Node CanonicRootTree(int[][] tree, int root)
        {
            var rootedTree = new Node(root, Array.Empty<Node>());
            return Dfs(tree, rootedTree, null);
        }

        private static Node Dfs(int[][] tree, Node node, Node parent)
        {
            foreach (var n in tree[node.Value])
            {
                if (parent != null && n == parent.Value)
                { continue; }
                var child = new Node(n, Array.Empty<Node>());
                child.Parent = node;
                node.Children.Add(child);
                Dfs(tree, child, node);
            }
            return node;
        }
    }
}
