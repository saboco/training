using System.Linq;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class BridgesAndArticulationPointsTests
    {
        [Fact]
        public void FindBridgesTest()
        {
            var n = 9;
            var g = Common.CreateEmptyGraph(n);

            Common.AddUndirectedEdge(g, 0, 1);
            Common.AddUndirectedEdge(g, 0, 2);
            Common.AddUndirectedEdge(g, 1, 2);
            Common.AddUndirectedEdge(g, 2, 3);
            Common.AddUndirectedEdge(g, 3, 4);
            Common.AddUndirectedEdge(g, 2, 5);
            Common.AddUndirectedEdge(g, 5, 6);
            Common.AddUndirectedEdge(g, 6, 7);
            Common.AddUndirectedEdge(g, 7, 8);
            Common.AddUndirectedEdge(g, 8, 5);

            var bridges = BridgesAndArticulationPoints.FindBridges(g.Select(l => l.ToArray()).ToArray());
            Assert.Equal(3, bridges.Length);
            Assert.Equal((3, 4), bridges[0]);
            Assert.Equal((2, 3), bridges[1]);
            Assert.Equal((2, 5), bridges[2]);
        }

        [Fact]
        public void FindArticulationPointsTest()
        {
            var n = 9;
            var g = Common.CreateEmptyGraph(n);

            Common.AddUndirectedEdge(g, 0, 1);
            Common.AddUndirectedEdge(g, 0, 2);
            Common.AddUndirectedEdge(g, 1, 2);
            Common.AddUndirectedEdge(g, 2, 3);
            Common.AddUndirectedEdge(g, 3, 4);
            Common.AddUndirectedEdge(g, 2, 5);
            Common.AddUndirectedEdge(g, 5, 6);
            Common.AddUndirectedEdge(g, 6, 7);
            Common.AddUndirectedEdge(g, 7, 8);
            Common.AddUndirectedEdge(g, 8, 5);

            var articulationPoints = BridgesAndArticulationPoints.FindArticulationPoints(g.Select(l => l.ToArray()).ToArray());
            var expected = new[] { false, false, true, true, false, true, false, false, false };
            Common.AssertEqual(expected, articulationPoints);
        }
    }
}
