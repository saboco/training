using System.Collections.Generic;

namespace Training.DataStructures
{
    public class Graph<TId>
    {
        public int NodesCount => _nodes.Count;

        private readonly Dictionary<TId, Node> _nodes = new Dictionary<TId, Node>();

        private class Node
        {
            public readonly ICollection<Node> AdjacentNodes = new List<Node>();
            public TId Id { get; }

            public Node(TId id)
            {
                Id = id;
            }

            public void AddAdjacentNode(Node adjacentNode)
            {
                AdjacentNodes.Add(adjacentNode);
            }
        }

        public bool ContainsNode(TId id)
        {
            return _nodes.ContainsKey(id);
        }

        public void AddNode(TId id)
        {
            if (!ContainsNode(id))
            {
                _nodes[id] = new Node(id);
            }
        }

        public void AddEdge(TId source, TId destination)
        {
            var sourceNode = _nodes.ContainsKey(source) ? _nodes[source] : new Node(source);
            var destinationNode = _nodes.ContainsKey(destination) ? _nodes[destination] : new Node(destination);

            sourceNode.AddAdjacentNode(destinationNode);

            if (!_nodes.ContainsKey(source))
            {
                _nodes.Add(source, sourceNode);
            }

            if (!_nodes.ContainsKey(destination))
            {
                _nodes.Add(destination, destinationNode);
            }
        }

        public Dictionary<TId, int> GetDistanceToAllNodesFrom(TId sourceId, int distanceBetweenEdges)
        {
            var distances = new Dictionary<TId, int>();
            var source = GetNode(sourceId);
            var nextNodesToVisite = new QueueOnStacks<Node>();
            var visitedNodes = new HashSet<Node>();

            distances.Add(source.Id, 0);
            nextNodesToVisite.Enqueue(source);
            while (!nextNodesToVisite.IsEmpty)
            {
                var node = nextNodesToVisite.Dequeue();

                if (visitedNodes.Contains(node))
                {
                    continue;
                }

                visitedNodes.Add(node);
                var previousDistance = distances[node.Id];

                foreach (var adjacentNode in node.AdjacentNodes)
                {
                    if (!distances.ContainsKey(adjacentNode.Id))
                    {
                        distances.Add(adjacentNode.Id, previousDistance + distanceBetweenEdges);
                    }

                    nextNodesToVisite.Enqueue(adjacentNode);
                }
            }

            return distances;
        }

        public bool HasPathBfs(TId source, TId destination)
        {
            return HasPathBfs(GetNode(source), GetNode(destination));
        }

        private static bool HasPathBfs(Node source, Node destination)
        {
            var nextNodesToVisite = new Queue<Node>();
            var visitedNodes = new HashSet<Node>();

            nextNodesToVisite.Enqueue(source);
            while (nextNodesToVisite.Count != 0)
            {
                var node = nextNodesToVisite.Dequeue();
                if (node == destination)
                {
                    return true;
                }

                if (visitedNodes.Contains(node))
                {
                    continue;
                }

                visitedNodes.Add(node);

                foreach (var adjacentNode in node.AdjacentNodes)
                {
                    nextNodesToVisite.Enqueue(adjacentNode);
                }
            }
            return false;
        }

        private Node GetNode(TId id)
        {
            return _nodes.ContainsKey(id) ? _nodes[id] : null;
        }

        public int GetGreatesRegion()
        {
            var visitedNodes = new HashSet<Node>();
            var greatesRegion = 0;
            foreach (var nextNode in _nodes.Values)
            {
                if (visitedNodes.Contains(nextNode))
                {
                    continue;
                }

                visitedNodes.Add(nextNode);

                var count = GetConnectedNodesCount(nextNode.Id);
                if (count <= greatesRegion)
                {
                    continue;
                }

                greatesRegion = count;
            }

            return greatesRegion;
        }

        public int GetConnectedNodesCount(TId source)
        {
            var visitedNodes = new HashSet<Node>();
            return GetConnectedNodesCount(GetNode(source), visitedNodes);
        }

        public bool HasPathDfs(TId source, TId destination)
        {
            var visitedNodes = new HashSet<Node>();
            return HasPathDfs(GetNode(source), GetNode(destination), visitedNodes);
        }


        private static int GetConnectedNodesCount(Node source, HashSet<Node> visitedNodes)
        {
            if (visitedNodes.Contains(source))
            {
                return visitedNodes.Count;
            }

            visitedNodes.Add(source);

            foreach (var adjacentNode in source.AdjacentNodes)
            {
                GetConnectedNodesCount(adjacentNode, visitedNodes);
            }
            return visitedNodes.Count;
        }

        private static bool HasPathDfs(Node source, Node destination, HashSet<Node> visitedNodes)
        {
            if (visitedNodes.Contains(source))
            {
                return false;
            }

            if (source == destination)
            {
                return true;
            }

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
    }
}