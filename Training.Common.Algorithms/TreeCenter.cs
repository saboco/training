using System.Collections.Generic;
using System.Linq;

namespace Training.Common.Algorithms
{
    public class TreeCenter
    {
        public static int[] FindCenter(int[][] g)
        {
            var graph = new Graph(g);
            Degree(graph);
            var leaves = new HashSet<Node>();
            FindCenter(graph, leaves);
            return graph.Nodes
                .Where(n => !leaves.Contains(n.Value))
                .Select(n => n.Value.Value)
                .ToArray();
        }

        public static int[] FindCenterWithoutHelperStructure(int[][] g)
        {
            var n = g.Length;
            var degree = new int[n];
            var leaves = new List<int>();
            for (var i = 0; i < g.Length; i++)
            {
                degree[i] = g[i].Length;
                if (degree[i] == 0 || degree[i] == 1)
                {
                    leaves.Add(i);
                }
            }
            var count = leaves.Count;
            while(count < n)
            { 
                var newLeaves = new List<int>();
                foreach(var leave in leaves)
                { 
                    foreach(var neighbor in g[leave])
                    { 
                        degree[neighbor] = degree[neighbor] - 1;
                        if(degree[neighbor]==1)
                        { 
                            newLeaves.Add(neighbor);
                        }
                    }
                    degree[leave]=0;
                }
                count += newLeaves.Count;
                leaves=newLeaves;
            }
            return leaves.ToArray();
        }

        private static void Degree(Graph g)
        {
            foreach (var n in g.Nodes)
            {
                n.Value.Degree = n.Value.Children.Count;
            }
        }

        private static void FindCenter(Graph g, HashSet<Node> leaves)
        {
            if (leaves.Count >= g.Nodes.Count - 2)
            {
                return;
            }

            foreach (var n in g.Nodes)
            {
                if (n.Value.Degree == 0)
                {
                    continue;
                }
                if (n.Value.Degree == 1)
                {
                    leaves.Add(n.Value);
                    n.Value.Degree = 0;
                }
            }
            foreach (var leave in leaves)
            {
                foreach (var child in leave.Children)
                {
                    child.Degree -= 1;
                }
            }
            FindCenter(g, leaves);
        }
    }
}
