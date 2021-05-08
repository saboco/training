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

        [Theory]
        [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 4)]
        [InlineData(new[] { 2, 3, 4, 5, 6, 7, 8, 9 }, 4)]
        [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 }, 4)]
        [InlineData(new[] { 2, 3, 6, 7, 8, 10, 12, 13 }, 4)]
        [InlineData(new[] { 16 }, 1)]
        [InlineData(new[] { 1, 6 }, 2)]
        [InlineData(new[] { 1, 6, 8 }, 2)]
        [InlineData(new[] { 1, 6, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 }, 5)]
        [InlineData(new int[0], 0)]
        public void FromArrayTest(int[] arr, int expectedHeight)
        {
            var tree = BinaryTree.FromArray(arr);
            Assert.Equal(expectedHeight, tree.Height());
        }

        [Theory]
        [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, new[] { 5 }, new[] { 2, 8 }, new[] { 1, 3, 6, 9 }, new[] { 4, 7, 10 })]
        [InlineData(new[] { 2, 3, 4, 5, 6, 7, 8, 9 }, new[] { 5 }, new[] { 3, 7 }, new[] { 2, 4, 6, 8 }, new[] { 9 })]
        [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 }, new[] { 7 }, new[] { 3, 10 }, new[] { 1, 5, 8, 12 }, new[] { 2, 4, 6, 9, 11, 13 })]
        [InlineData(new[] { 2, 3, 6, 7, 8, 10, 12, 13 }, new[] { 7 }, new[] { 3, 10 }, new[] { 2, 6, 8, 12 }, new[] { 13 })]
        [InlineData(new[] { 16 }, new[] { 16 })]
        [InlineData(new[] { 1, 6 }, new[] { 1 }, new[] { 6 })]
        [InlineData(new[] { 1, 6, 8 }, new[] { 6 }, new[] { 1, 8 })]
        [InlineData(new[] { 1, 6, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 },
            new[] { 18 },
            new[] { 11, 24 },
            new[] { 8, 14, 21, 27 },
            new[] { 1, 9, 12, 16, 19, 22, 25, 29 },
            new[] { 6, 10, 13, 15, 17, 20, 23, 26, 28, 30 })]
        [InlineData(new int[0], null)]
        public void GetLevelsTest(int[] arr, params int[][] expectedLevels)
        {
            var bst = BinarySearchTree.FromArray(arr);
            var actual = bst.GetLevels();
            if (expectedLevels != null)
            {
                Assert.Equal(expectedLevels.Length, actual.Count);
                for (var i = 0; i < expectedLevels.Length; i++)
                {
                    Assert.Equal(actual[i].Count, expectedLevels[i].Length);
                    var current = actual[i].First;
                    for (var j = 0; j < expectedLevels[i].Length; j++)
                    {
                        Assert.Equal(current.Value.Data, expectedLevels[i][j]);
                        current = current.Next;
                    }
                }
            }
            else
            {
                Assert.Empty(actual);
            }
        }
    }
}
