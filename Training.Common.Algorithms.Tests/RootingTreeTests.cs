using System;
using System.Collections.Generic;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class RootingTreeTests
    {
        [Fact]
        public void RootingTreeTest()
        {
            var root = 0;
            var tree = new[]
            { 
                new []{ 5, 1, 2},
                new []{ 0 },
                new []{ 3, 0},
                new []{ 2 },
                new []{ 5},
                new []{ 0,6,4},
                new[] { 5 } 
            };
            var node6 = new Node(6, Array.Empty<Node>());
            var node4 = new Node(4, Array.Empty<Node>());
            var node5 = new Node(5, new [] { node6, node4 });
            var node1 = new Node(1, Array.Empty<Node>());
            var node3 = new Node(3, Array.Empty<Node>());
            var node2 = new Node(2, new []{node3 });
            var expectedTree = new Node(0, new []{ node5, node1, node2 });

            var rootedTree = TreeRooting.RootTree(tree, root);
            AssertTree(expectedTree, rootedTree);
            var canonic = TreeRooting.CanonicRootTree(tree, root);
            AssertTree(expectedTree, canonic);
        }

        private void AssertTree(Node a, Node b)
        { 
            Assert.Equal(a is not null, b is not null);

            var qA = new Queue<Node>();
            var qB = new Queue<Node>();
            var visitedA = new HashSet<int>();
            var visitedB = new HashSet<int>();
            qA.Enqueue(a);
            qB.Enqueue(b);
            visitedA.Add(a.Value);
            visitedB.Add(b.Value);

            while(qA.Count == qB.Count && qA.Count > 0)
            { 
                var currentA = qA.Dequeue();
                var currentB = qB.Dequeue();
                Assert.Equal(currentA.Value, currentB.Value);
                
                foreach(var cA in currentA.Children)
                { 
                    if(!visitedA.Contains(cA.Value))
                    { 
                        visitedA.Add(cA.Value);
                        qA.Enqueue(cA);
                    }
                }
                foreach(var cB in currentB.Children)
                { 
                    if(!visitedB.Contains(cB.Value))
                    { 
                        visitedB.Add(cB.Value);
                        qB.Enqueue(cB);
                    }
                }
            }
            Assert.Equal(qA.Count, qB.Count);
        }
    }
}
