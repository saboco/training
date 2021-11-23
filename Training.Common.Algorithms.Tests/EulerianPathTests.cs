using System.Linq;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class EulerianPathTests
    {
        [Fact]
        public void FindEulerianPathTest()
        {
            var g = Common.CreateEmptyGraph(7);
            Common.AddDirectedEdge(g, 1, 2);
            Common.AddDirectedEdge(g, 1, 3);
            Common.AddDirectedEdge(g, 2, 2);
            Common.AddDirectedEdge(g, 2, 4);
            Common.AddDirectedEdge(g, 2, 4);
            Common.AddDirectedEdge(g, 3, 1);
            Common.AddDirectedEdge(g, 3, 2);
            Common.AddDirectedEdge(g, 3, 5);
            Common.AddDirectedEdge(g, 4, 6);
            Common.AddDirectedEdge(g, 4, 3);
            Common.AddDirectedEdge(g, 5, 6);
            Common.AddDirectedEdge(g, 6, 3);

            var eulerianPath = EulerianPath.FindEulerianPath(g.Select(l => l.ToArray()).ToArray());
            var expected = new[] { 1, 3, 5, 6, 3, 2, 4, 3, 1, 2, 2, 4, 6 };
            Common.AssertEqual(expected, eulerianPath);
        }
    }
}
