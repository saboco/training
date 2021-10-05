using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class TreeHeightTests
    {
        [Fact]
        public void BinaryTreeHeightTest()
        {
            var node5 = new BinaryNode(5, null, null);
            var node4 = new BinaryNode(4, node5, null);
            var node3 = new BinaryNode(3, null, null);
            var node2 = new BinaryNode(2, null, null);
            var node1 = new BinaryNode(1, node3, node4);
            var root = new BinaryNode(0, node1, node2);
            var height = TreeHeight.Height(root);
            Assert.Equal(3, height);
        }

        [Fact]
        public void TreeHeightTest()
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
            var height = TreeHeight.Height(root);
            Assert.Equal(3, height);
        }
    }
}
