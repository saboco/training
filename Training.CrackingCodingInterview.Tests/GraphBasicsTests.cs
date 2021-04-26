using System.Text;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class GraphBasicsTests
    {
        [Fact]
        public void DfsTest()
        {
            var graph = new Graph(8);
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(0, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 5);
            graph.AddEdge(3, 6);
            graph.AddEdge(4, 7);
            graph.AddEdge(5, 7);
            graph.AddEdge(6, 7);

            var sb = new StringBuilder("|");
            graph.Dfs(0, node => sb.Append($"{node}|"));
            Assert.Equal("|0|1|4|7|2|5|3|6|", sb.ToString());
        }

        [Fact]
        public void DfsTest2()
        {
            var graph = new Graph(4);

            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 0);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 3);

            var sb = new StringBuilder("|");
            graph.Dfs(2, node => sb.Append($"{node}|"));
            Assert.Equal("|2|0|1|3|", sb.ToString());
        }

        [Fact]
        public void DfsOnDisconnectedGraph()
        {
            Graph graph = new Graph(4);

            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 0);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 3);

            var sb = new StringBuilder("|");
            graph.Dfs(0, node => sb.Append($"{node}|"));
            Assert.Equal("|0|1|2|3|", sb.ToString());
        }

        [Fact]
        public void DfsOnDisconnectedGraph2()
        {
            Graph graph = new Graph(7);

            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 0);

            graph.AddEdge(3, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 5);
            graph.AddEdge(4, 6);
            graph.AddEdge(6, 3);

            var sb = new StringBuilder("|");
            graph.Dfs(0, node => sb.Append($"{node}|"));
            Assert.Equal("|0|1|2|3|4|5|6|", sb.ToString());
        }

        [Fact]
        public void BfsTest()
        {
            Graph graph = new Graph(4);

            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 0);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 3);
            var sb = new StringBuilder("|");
            graph.Bfs(2, node => sb.Append($"{node}|"));
            Assert.Equal("|2|0|3|1|", sb.ToString());
        }

        [Fact]
        public void BfsOnDisconnectedGraph2()
        {
            Graph graph = new Graph(7);

            graph.AddEdge(0, 2);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);

            graph.AddEdge(3, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 5);
            graph.AddEdge(4, 6);
            graph.AddEdge(6, 3);

            var sb = new StringBuilder("|");
            graph.Bfs(0, node => sb.Append($"{node}|"));
            Assert.Equal("|0|2|1|3|4|5|6|", sb.ToString());
        }
    }
}