using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class SumLeafNodesTests
    {
        [Fact]
        public void SumLeafNodesTest()
        { 
            var emptyChildre = System.Array.Empty<Node>();
            var node2 = new Node(2, emptyChildre);
            var node9 = new Node(9, emptyChildre);
            var node1 = new Node(1, new []{ node2, node9});
            var nodeN6 = new Node(-6, emptyChildre);
            var node4 = new Node(4, new[]{node1, nodeN6});
            var node8 = new Node(8, emptyChildre);
            var node0 = new Node(0, emptyChildre);
            var node7 = new Node(7, new []{ node8 });
            var nodeN4 = new Node(-4, emptyChildre);
            var node3 = new Node(3, new []{node0, node7, nodeN4 });
            var root = new Node(5, new []{node4, node3 });
            var sum = SumLeafNodes.Sum(root);
            Assert.Equal(9, sum);
        }
    }
}
