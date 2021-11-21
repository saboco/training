using System.Linq;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class TarjansStronglyConnectedComponentsTests
    {
        [Fact]
        public void FindStronglyConnectedComponentsTest()
        {
            var g = Common.CreateEmptyGraph(8);
            Common.AddDrectedEdge(g, 6, 0);
            Common.AddDrectedEdge(g, 6, 2);
            Common.AddDrectedEdge(g, 3, 4);
            Common.AddDrectedEdge(g, 6, 4);
            Common.AddDrectedEdge(g, 2, 0);
            Common.AddDrectedEdge(g, 0, 1);
            Common.AddDrectedEdge(g, 4, 5);
            Common.AddDrectedEdge(g, 5, 6);
            Common.AddDrectedEdge(g, 3, 7);
            Common.AddDrectedEdge(g, 7, 5);
            Common.AddDrectedEdge(g, 1, 2);
            Common.AddDrectedEdge(g, 7, 3);
            Common.AddDrectedEdge(g, 5, 0);

            var (count,scc) = TarjansStronglyConnectedComponents.FindStronglyConnectedComponents(g.Select(l => l.ToArray()).ToArray());

            Assert.Equal(3, count);
            Assert.Equal(0, scc[0]);
            Assert.Equal(0, scc[1]);
            Assert.Equal(0, scc[2]);

            Assert.Equal(3, scc[3]);
            Assert.Equal(3, scc[7]);

            Assert.Equal(4, scc[4]);
            Assert.Equal(4, scc[5]);
            Assert.Equal(4, scc[6]);
        }
    }
}
