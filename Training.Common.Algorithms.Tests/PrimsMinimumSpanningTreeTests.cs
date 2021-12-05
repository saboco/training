using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class PrimsMinimumSpanningTreeTests
    {

        [Fact]
        public void MinimumSpanningTreeTest()
        {
            var n = 7;
            var g = Common.CreateEmptyWeightedGraph(n);

            Common.AddUndirectedWeightedEdge(g, 0, 1, 9);
            Common.AddUndirectedWeightedEdge(g, 0, 2, 0);
            Common.AddUndirectedWeightedEdge(g, 0, 3, 5);
            Common.AddUndirectedWeightedEdge(g, 0, 5, 7);
            Common.AddUndirectedWeightedEdge(g, 1, 3, -2);
            Common.AddUndirectedWeightedEdge(g, 1, 4, 3);
            Common.AddUndirectedWeightedEdge(g, 1, 6, 4);
            Common.AddUndirectedWeightedEdge(g, 2, 5, 6);
            Common.AddUndirectedWeightedEdge(g, 3, 5, 2);
            Common.AddUndirectedWeightedEdge(g, 3, 6, 3);
            Common.AddUndirectedWeightedEdge(g, 4, 6, 6);
            Common.AddUndirectedWeightedEdge(g, 5, 6, 1);

            var (cost, spanningTree) = PrimsMinimumSpanningTree.MinimumSpanningTree(g.Select(l => l.ToArray()).ToArray());
            Assert.Equal(9, cost);

        }

        [Fact]
        public void MinimumSpanningTreeTest2()
        {
            var n = 8;
            var g = Common.CreateEmptyWeightedGraph(n);

            Common.AddUndirectedWeightedEdge(g, 0, 1, 10);
            Common.AddUndirectedWeightedEdge(g, 0, 2, 1);
            Common.AddUndirectedWeightedEdge(g, 0, 3, 4);
            
            Common.AddUndirectedWeightedEdge(g, 1, 2, 3);
            Common.AddUndirectedWeightedEdge(g, 1, 4, 0);

            Common.AddUndirectedWeightedEdge(g, 2, 3, 2);
            Common.AddUndirectedWeightedEdge(g, 2, 5, 8);

            Common.AddUndirectedWeightedEdge(g, 3, 5, 2);
            Common.AddUndirectedWeightedEdge(g, 3, 6, 7);
                        
            Common.AddUndirectedWeightedEdge(g, 4, 5, 1);
            Common.AddUndirectedWeightedEdge(g, 4, 7, 8);

            Common.AddUndirectedWeightedEdge(g, 5, 6, 6);
            Common.AddUndirectedWeightedEdge(g, 5, 7, 9);

            Common.AddUndirectedWeightedEdge(g, 6, 7, 12);

            var (cost, spanningTree) = PrimsMinimumSpanningTree.MinimumSpanningTree(g.Select(l => l.ToArray()).ToArray());
            Assert.Equal(20, cost);

        }
    }
}
