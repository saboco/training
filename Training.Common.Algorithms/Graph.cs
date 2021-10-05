using System;
using System.Collections.Generic;

namespace Training.Common.Algorithms
{
    public class Graph
    {
        public Dictionary<int, Node> Nodes = new Dictionary<int,Node>();

        public Graph(int[][] g)
        {
            for (var i = 0; i < g.Length; i++)
            {
                AddNodes(i, g[i]);
            }
        }

        private void AddNode(int id, int to)
        {
            if (!Nodes.ContainsKey(to))
            {
                Nodes.Add(to, new Node(to, Array.Empty<Node>()));
            }
            if (!Nodes.ContainsKey(id))
            {
                Nodes.Add(id, new Node(id, Array.Empty<Node>()));
            }
            Nodes[to].Children.Add(Nodes[id]);
            Nodes[id].Parent = Nodes[to];
        }

        private void AddNodes(int id, int[] children)
        {
            foreach (var child in children)
            {
                AddNode(child, id);
            }
        }
    }
}
