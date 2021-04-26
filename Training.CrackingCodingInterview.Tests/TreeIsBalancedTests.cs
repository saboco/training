using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BNode = Training.CrackingCodingInterview.TreeIsBalanced.Node;

namespace Training.CrackingCodingInterview.Tests
{
    public class TreeIsBalancedTests
    {
        [Fact]
        public void IsBalancedTest()
        {
            var root = new BNode(50);
            var n1 = new BNode(30);
            var n2 = new BNode(20);
            var n3 = new BNode(40);
            var n4 = new BNode(70);
            var n5 = new BNode(60);
            var n6 = new BNode(80);
            root.Left = n1;
            root.Right = n4;
            n1.Left = n2;
            n1.Right = n3;
            n4.Left = n5;
            n4.Right = n6;
            var tree = new TreeIsBalanced(root);
            Assert.True(tree.IsBalanced());
        }

        [Fact]
        public void IsNotBalancedTest()
        {
            var root = new BNode(50);
            var n1 = new BNode(30);
            var n2 = new BNode(20);
            var n3 = new BNode(40);

            root.Left = n1;
            n1.Left = n2;
            n1.Right = n3;

            var tree = new TreeIsBalanced(root);
            Assert.False(tree.IsBalanced());
        }

        [Fact]
        public void IsBalanced2Test()
        {
            var root = new BNode(50);
            var n1 = new BNode(30);
            var n2 = new BNode(20);
            var n3 = new BNode(40);
            var n4 = new BNode(70);
            var n5 = new BNode(60);
            var n6 = new BNode(80);
            root.Left = n1;
            root.Right = n4;
            n1.Left = n2;
            n1.Right = n3;
            n4.Left = n5;
            n4.Right = n6;
            var tree = new TreeIsBalanced(root);
            Assert.True(tree.IsBalanced2());
        }

        [Fact]
        public void IsNotBalanced2Test()
        {
            var root = new BNode(50);
            var n1 = new BNode(30);
            var n2 = new BNode(20);
            var n3 = new BNode(40);

            root.Left = n1;
            n1.Left = n2;
            n1.Right = n3;

            var tree = new TreeIsBalanced(root);
            Assert.False(tree.IsBalanced2());
        }

         [Fact]
        public void IsBalanced3Test()
        {
            var root = new BNode(50);
            var n1 = new BNode(30);
            var n2 = new BNode(20);
            var n3 = new BNode(40);
            var n4 = new BNode(70);
            var n5 = new BNode(60);
            var n6 = new BNode(80);
            root.Left = n1;
            root.Right = n4;
            n1.Left = n2;
            n1.Right = n3;
            n4.Left = n5;
            n4.Right = n6;
            var tree = new TreeIsBalanced(root);
            Assert.True(tree.IsBalanced3());
        }

        [Fact]
        public void IsNotBalanced3Test()
        {
            var root = new BNode(50);
            var n1 = new BNode(30);
            var n2 = new BNode(20);
            var n3 = new BNode(40);

            root.Left = n1;
            n1.Left = n2;
            n1.Right = n3;

            var tree = new TreeIsBalanced(root);
            Assert.False(tree.IsBalanced3());
        }
    }
}
