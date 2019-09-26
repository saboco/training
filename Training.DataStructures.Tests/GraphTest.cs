using Xunit;

namespace Training.DataStructures.Tests
{
    public class GraphTest
    {
        [Fact]
        public void Should_return_correct_answer_when_asked_for_an_existing_path_by_dfs()
        {
            var graph = new Graph<int>();
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 2);
            graph.AddEdge(3, 5);
            graph.AddEdge(3, 21);
            graph.AddEdge(5, 9);
            graph.AddEdge(9, 21);
            graph.AddEdge(21, 15);

            var hasPath1To15 = graph.HasPathDfs(1, 15);
            Assert.True(hasPath1To15);
            var hasPath3To9 = graph.HasPathDfs(3, 9);
            Assert.True(hasPath3To9);

            var hasPath2To15 = graph.HasPathDfs(2, 15);
            Assert.False(hasPath2To15);
            var hasPath15To21 = graph.HasPathDfs(15, 21);
            Assert.False(hasPath15To21);
        }

        [Fact]
        public void Should_return_correct_answer_when_asked_for_an_existing_path_by_bfs()
        {
            var graph = new Graph<int>();
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 2);
            graph.AddEdge(3, 5);
            graph.AddEdge(3, 21);
            graph.AddEdge(5, 9);
            graph.AddEdge(9, 21);
            graph.AddEdge(21, 15);


            var hasPath1To15 = graph.HasPathBfs(1, 15);
            Assert.True(hasPath1To15);
            var hasPath3To9 = graph.HasPathBfs(3, 9);
            Assert.True(hasPath3To9);

            var hasPath2To15 = graph.HasPathBfs(2, 15);
            Assert.False(hasPath2To15);
            var hasPath15To21 = graph.HasPathBfs(15, 21);
            Assert.False(hasPath15To21);
        }

        [Fact]
        public void Should_return_number_of_connected_nodes_when_asked_for_it()
        {
            var graph = new Graph<int>();
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 2);
            graph.AddEdge(3, 5);
            graph.AddEdge(3, 21);
            graph.AddEdge(5, 9);
            graph.AddEdge(9, 21);
            graph.AddEdge(21, 15);

            graph.AddEdge(22, 16);

            var count = graph.GetConnectedNodesCount(1);
            Assert.Equal(7, count);
            var countFrom22 = graph.GetConnectedNodesCount(22);
            Assert.Equal(2, countFrom22);

            var graph2 = new Graph<int>();
            graph2.AddEdge(1, 3);
            graph2.AddEdge(1, 2);
            graph2.AddEdge(3, 5);

            graph2.AddEdge(22, 16);

            var count2From1 = graph2.GetConnectedNodesCount(1);
            Assert.Equal(4, count2From1);
            var count2From22 = graph2.GetConnectedNodesCount(22);
            Assert.Equal(2, count2From22);
        }
    }
}