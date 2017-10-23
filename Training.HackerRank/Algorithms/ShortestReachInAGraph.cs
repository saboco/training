using System;
using System.Collections.Generic;
using System.Linq;
using Training.Common;
using Training.DataStructures;

namespace Training.HackerRank.Algorithms
{
    public class ShortestReachInAGraph
    {
        private readonly Graph<int> _graph = new Graph<int>();

        public ShortestReachInAGraph(IEnumerable<Edge> edges, int numberOfNodes)
        {
            foreach (var edge in edges)
            {
                _graph.AddEdge(edge.LeftNode, edge.RightNode);
                _graph.AddEdge(edge.RightNode, edge.LeftNode);
            }

            foreach (var node in Enumerable.Range(1, numberOfNodes))
            {
                _graph.AddNode(node);
            }
        }

        public bool ContainsNode(int id)
        {
            return _graph.ContainsNode(id);
        }

        public bool HasPathBetween(int node1, int node2)
        {
            return _graph.HasPathBfs(node1, node2);
        }

        public IEnumerable<int> GetShortestPathToAllNodesFrom(int queryStartingNode)
        {
            var distances = _graph.GetDistanceToAllNodesFrom(queryStartingNode, 6);
            foreach (var nodeId in Enumerable.Range(1, _graph.NodesCount))
            {
                if(nodeId == queryStartingNode) continue;
                if (distances.ContainsKey(nodeId))
                    yield return distances[nodeId];
                else
                    yield return -1;
            }
        }
    }

    public static class InputParser
    {
        public static Queries Parse(IReadLines linesReader)
        {
            var queriesNumber = Convert.ToInt32(linesReader.ReadLine());
            var queries = new Queries(queriesNumber);
            for (var i = 0; i < queriesNumber; i++)
            {
                var numberOfNodesAndEdges = linesReader.ReadLine().Split(' ');
                var numberOfNodes = Convert.ToInt32(numberOfNodesAndEdges[0]);
                var numberOfEdges = Convert.ToInt32(numberOfNodesAndEdges[1]);
                var edges = new Edge[numberOfEdges];
                for (var j = 0; j < numberOfEdges; j++)
                {
                    var edgeNodes = linesReader.ReadLine().Split(' ');
                    var leftNode = Convert.ToInt32(edgeNodes[0]);
                    var rightNode = Convert.ToInt32(edgeNodes[1]);
                    edges[j] = new Edge(leftNode, rightNode);
                }
                var startingNode = Convert.ToInt32(linesReader.ReadLine());
                queries.Add(numberOfEdges, numberOfNodes, edges, startingNode);
            }

            return queries;
        }
    }

    public class Edge
    {
        public int LeftNode { get; }
        public int RightNode { get; }

        public Edge(int leftNode, int rightNode)
        {
            LeftNode = leftNode;
            RightNode = rightNode;
        }
    }

    public class Queries
    {
        public int Count { get; }
        public IEnumerable<Query> Items => _queries;

        private readonly List<Query> _queries;

        public Queries(int queriesNumber)
        {
            _queries = new List<Query>();

            Count = queriesNumber;
        }

        public void Add(int numberOfEdges, int numberOfNodes, Edge[] edges, int startingNode)
        {
            _queries.Add(new Query(numberOfEdges, numberOfNodes, edges, startingNode));
        }
    }

    public class Query
    {
        public int NumberOfEdges { get; }
        public int NumberOfNodes { get; }
        public Edge[] Edges { get; }
        public int StartingNode { get; }

        public Query(int numberOfEdges, int numberOfNodes, Edge[] edges, int startingNode)
        {
            NumberOfEdges = numberOfEdges;
            NumberOfNodes = numberOfNodes;
            Edges = edges;
            StartingNode = startingNode;
        }
    }

    public static class QueriesTreater
    {
        public static string[] Treat(Queries queries)
        {
            var output = new List<string>();
            foreach (var query in queries.Items)
            {
                output.Add(TreatQuery(query));
            }
            return output.ToArray();
        }

        private static string TreatQuery(Query query)
        {
            var shortestReachInGraph = new ShortestReachInAGraph(query.Edges, query.NumberOfNodes);
            return string.Join(" ", shortestReachInGraph.GetShortestPathToAllNodesFrom(query.StartingNode));
        }
    }
}