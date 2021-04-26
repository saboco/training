using System.Collections.Generic;
using System.Text;
using Xunit;
using BNode = Training.CrackingCodingInterview.BinaryTree.Node;

namespace Training.CrackingCodingInterview.Tests
{
    public class BinaryTreeTests
    {
        [Fact]
        public void BinaryTreeTest()
        {
            var n0 = new BNode(0);
            var n1 = new BNode(1);
            var n2 = new BNode(2);
            var n3 = new BNode(3);
            var n4 = new BNode(4);
            var n5 = new BNode(5);
            var n6 = new BNode(6);
            var n7 = new BNode(7);
            var n8 = new BNode(8);
            var n9 = new BNode(9);

            n3.Left = n1;
            n3.Right = n2;
            n8.Left = n3;
            n8.Right = n4;
            n7.Left = n8;
            n7.Right = n6;
            n6.Left = n9;
            n6.Right = n0;
            n9.Left = n5;

            var tree = new BinaryTree(n7);

            var sb = new StringBuilder();
            tree.InOrderTraverse(s => sb.Append(s.Data));
            Assert.Equal("1328475960", sb.ToString());
            sb.Clear();
            tree.PreOrderTraverse(s => sb.Append(s.Data));
            Assert.Equal("7831246950", sb.ToString());
            sb.Clear();
            tree.PostOrderTraverse(s => sb.Append(s.Data));
            Assert.Equal("1234859067", sb.ToString());
        }

        [Fact]
        public void BinarySearchTreeTest1()
        {
            var nodes = new List<BNode>
            {
                new BNode(30),
                new BNode(20),
                new BNode(40),
                new BNode(70),
                new BNode(60),
                new BNode(80)
            };

            var root = new BNode(50);

            var bst = new BinarySearchTree(root);
            foreach (var node in nodes)
            {
                bst.Insert(node.Data);
            }

            var sb = new StringBuilder("|");
            bst.InOrderTraverse(n => sb.Append($"{n.Data}|"));
            Assert.Equal("|20|30|40|50|60|70|80|", sb.ToString());
        }

        [Fact]
        public void BinarySearchTreeTest2()
        {
            var nodes = new List<BNode>
            {
                new BNode(0),
                new BNode(1),
                new BNode(2),
                new BNode(3),
                new BNode(4),
                new BNode(5),
                new BNode(6),
                new BNode(8),
                new BNode(9)
            };

            var root = new BNode(7);

            var bst = new BinarySearchTree(root);
            foreach (var node in nodes)
            {
                bst.Insert(node.Data);
            }

            var sb = new StringBuilder();
            bst.InOrderTraverse(n => sb.Append(n.Data));
            Assert.Equal("0123456789", sb.ToString());
        }

        [Fact]
        public void BinarySearchTreeInOrderTraversalWithStack()
        {
            var nodes = new List<BNode>
            {
                new BNode(30),
                new BNode(20),
                new BNode(40),
                new BNode(70),
                new BNode(60),
                new BNode(80)
            };

            var root = new BNode(50);

            var bst = new BinarySearchTree(root);
            foreach (var node in nodes)
            {
                bst.Insert(node.Data);
            }

            var sb = new StringBuilder("|");
            bst.InOrderTraverseWithStack(n => sb.Append($"{n.Data}|"));
            Assert.Equal("|20|30|40|50|60|70|80|", sb.ToString());
        }

        [Fact]
        public void BinaryTreeTraversalsWithStack()
        {
            var n0 = new BNode(0);
            var n1 = new BNode(1);
            var n2 = new BNode(2);
            var n3 = new BNode(3);
            var n4 = new BNode(4);
            var n5 = new BNode(5);
            var n6 = new BNode(6);
            var n7 = new BNode(7);
            var n8 = new BNode(8);
            var n9 = new BNode(9);

            n3.Left = n1;
            n3.Right = n2;
            n8.Left = n3;
            n8.Right = n4;
            n7.Left = n8;
            n7.Right = n6;
            n6.Left = n9;
            n6.Right = n0;
            n9.Left = n5;

            var tree = new BinaryTree(n7);

            var sb = new StringBuilder();
            //tree.InOrderTraverseWithStack(s => sb.Append(s.Data));
            //Assert.Equal("1328475960", sb.ToString());
            //sb.Clear();
            //tree.PreOrderTraverseWithStack(s => sb.Append(s.Data));
            //Assert.Equal("7831246950", sb.ToString());
            //sb.Clear();
            tree.PostOrderTraverseWithStack(s => sb.Append(s.Data));
            Assert.Equal("1234859067", sb.ToString());
        }

        [Fact]
        public void LevelOrderTraversal()
        {
            var nodes = new List<BNode>
            {
                new BNode(30),
                new BNode(20),
                new BNode(40),
                new BNode(70),
                new BNode(60),
                new BNode(80)
            };

            var root = new BNode(50);

            var bst = new BinarySearchTree(root);
            foreach (var node in nodes)
            {
                bst.Insert(node.Data);
            }

            var sb = new StringBuilder("|");
            bst.LevelOrderTraverse(n => sb.Append($"{n.Data}|"));
            Assert.Equal("|50|30|70|20|40|60|80|", sb.ToString());
        }
    }
}
