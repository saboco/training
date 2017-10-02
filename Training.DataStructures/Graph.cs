using System.Collections.Generic;
using System.Linq;

namespace Training.DataStructures
{
    public class Graph<T>
    {
        private readonly Dictionary<T, Node> _nodes = new Dictionary<T, Node>();

        public class Node
        {
            public readonly ICollection<Node> AdjacentNodes = new List<Node>();
            public T Id { get; }

            public Node(T id)
            {
                Id = id;
            }

            public void AddAdjacentNode(Node adjacentNode)
            {
                AdjacentNodes.Add(adjacentNode);
            }
        }

        public void AddNode(T id)
        {
            if (!_nodes.ContainsKey(id)) _nodes[id] = new Node(id);
        }

        public void AddEdge(T source, T destination)
        {
            var sourceNode = _nodes.ContainsKey(source) ? _nodes[source] : new Node(source);
            var destinationNode = _nodes.ContainsKey(destination) ? _nodes[destination] : new Node(destination);

            sourceNode.AddAdjacentNode(destinationNode);

            if (!_nodes.ContainsKey(source))
                _nodes.Add(source, sourceNode);
            if (!_nodes.ContainsKey(destination))
                _nodes.Add(destination, destinationNode);
        }

        public bool HasPathDfs(T source, T destination)
        {
            var visitedNodes = new HashSet<Node>();
            return HasPathDfs(GetNode(source), GetNode(destination), visitedNodes);
        }

        public int GetGreatesRegion()
        {
            var visitedNodes = new HashSet<Node>();
            var greatesRegion = 0;
            foreach (var nextNode in _nodes.Select(kvNode => kvNode.Value))
            {
                if (visitedNodes.Contains(nextNode)) continue;
                visitedNodes.Add(nextNode);

                var count = GetConnectedNodesCount(nextNode.Id);
                if (count <= greatesRegion) continue;

                greatesRegion = count;
            }

            return greatesRegion;
        }

        public int GetConnectedNodesCount(T source)
        {
            var visitedNodes = new HashSet<Node>();
            return GetConnectedNodesCount(GetNode(source), visitedNodes);
        }

        private Node GetNode(T id)
        {
            return _nodes.ContainsKey(id) ? _nodes[id] : null;
        }

        private static int GetConnectedNodesCount(Node source, HashSet<Node> visitedNodes)
        {
            if (visitedNodes.Contains(source)) return visitedNodes.Count;
            visitedNodes.Add(source);

            foreach (var adjacentNode in source.AdjacentNodes)
            {
                GetConnectedNodesCount(adjacentNode, visitedNodes);
            }
            return visitedNodes.Count;
        }

        private static bool HasPathDfs(Node source, Node destination, HashSet<Node> visitedNodes)
        {
            if (visitedNodes.Contains(source)) return false;
            if (source == destination) return true;

            visitedNodes.Add(source);
            foreach (var nextNode in source.AdjacentNodes)
            {
                if (HasPathDfs(nextNode, destination, visitedNodes))
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasPathBfs(T source, T destination)
        {
            return HasPathBfs(GetNode(source), GetNode(destination));
        }

        private static bool HasPathBfs(Node source, Node destination)
        {
            var nextNodesToVisite = new Queue<Node>();
            var visitedNodes = new HashSet<Node>();

            nextNodesToVisite.Enqueue(source);
            while (!nextNodesToVisite.IsEmpty)
            {
                var node = nextNodesToVisite.Dequeue();
                if (node == destination) return true;
                if (visitedNodes.Contains(node)) continue;
                visitedNodes.Add(node);

                foreach (var adjacentNode in node.AdjacentNodes)
                {
                    nextNodesToVisite.Enqueue(adjacentNode);
                }
            }
            return false;
        }
    }
}