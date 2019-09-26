using System.Collections.Generic;
using System.Linq;
using Xunit;
using Training.HackerRank.Algorithms;

namespace Training.HackerRank.Tests.Algorithms
{    
    public class ShortestReachInAGraphTests
    {
        [Theory]
        [InlineData(5, new[] {4, 2}, new[] {1, 2}, new[] {1, 3})]
        public void Should_create_an_undirected_graph_with_all_nodes(int numberOfNodes, params int[][] rawEdges)
        {
            var edges = new List<Edge>();
            foreach (var rawEdge in rawEdges)
            {
                edges.Add(new Edge(rawEdge[0], rawEdge[1]));
            }

            var shortestReach = new ShortestReachInAGraph(edges, numberOfNodes);
            foreach (var edge in edges)
            {
                Assert.True(shortestReach.HasPathBetween(edge.LeftNode, edge.RightNode));
                Assert.True(shortestReach.HasPathBetween(edge.RightNode, edge.LeftNode));
            }

            foreach (var nodeId in Enumerable.Range(1, numberOfNodes))
            {
                Assert.True(shortestReach.ContainsNode(nodeId));
            }
        }

        [Theory]
        [InlineData("2\r\n4 2\r\n1 2\r\n1 3\r\n1\r\n3 1\r\n2 3\r\n2")]
        public void Should_be_able_to_parse_the_input(string input)
        {
            using (var linesReader = new FakeLinesReader(input))
            {
                var queries = InputParser.Parse(linesReader);

                Assert.Equal(2, queries.Count);

                var items = queries.Items.ToArray();
                var query1 = items[0];
                var query2 = items[1];

                Assert.Equal(4, query1.NumberOfNodes);
                Assert.Equal(2, query1.NumberOfEdges);
                Assert.Equal(1, query1.StartingNode);

                Assert.Equal(1, query1.Edges[0].LeftNode);
                Assert.Equal(2, query1.Edges[0].RightNode);

                Assert.Equal(1, query1.Edges[1].LeftNode);
                Assert.Equal(3, query1.Edges[1].RightNode);

                //**************************
                Assert.Equal(3, query2.NumberOfNodes);
                Assert.Equal(1, query2.NumberOfEdges);
                Assert.Equal(2, query2.StartingNode);

                Assert.Equal(2, query2.Edges[0].LeftNode);
                Assert.Equal(3, query2.Edges[0].RightNode);
            }
        }

        [Theory]
        [InlineData("2\r\n4 2\r\n1 2\r\n1 3\r\n1\r\n3 1\r\n2 3\r\n2")]
        public void Should_treat_queries_and_give_expected_result(string input)
        {
            using (var linesReader = new FakeLinesReader(input))
            {
                var queries = InputParser.Parse(linesReader);
                var output = QueriesTreater.Treat(queries);

                Assert.Equal("6 6 -1", output[0]);
                Assert.Equal("-1 6", output[1]);
            }
        }

        [Fact]
        public void Should_treat_big_queries()
        {
            const string input = ShortestReachInAGraphTestData.Input;
            using (var linesReader = new FakeLinesReader(input))
            {
                var queries = InputParser.Parse(linesReader);
                var output = QueriesTreater.Treat(queries);

                Assert.Equal(ShortestReachInAGraphTestData.Output1, output[0]);
                Assert.Equal(ShortestReachInAGraphTestData.Output2, output[1]);
            }
        }
    }
}