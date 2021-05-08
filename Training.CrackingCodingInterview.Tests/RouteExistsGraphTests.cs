using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class RouteExistsGraphTests
    {
        [Theory]
        [InlineData(0, 2, true, new[] { 1, 2 }, new[] { 2 }, new int[0], new[] { 3, 4 }, new[] { 5, 6 }, new int[0], new[] { 3 })]
        [InlineData(0, 6, false, new[] { 1, 2 }, new[] { 2 }, new int[0], new[] { 3, 4 }, new[] { 5, 6 }, new int[0], new[] { 3 })]
        public void RouteExistsGraphTest(int src, int dst, bool expected, params int[][] nodes)
        {
            var graph = new RouteExistsGraph(nodes.Length);
            for (var i = 0; i < nodes.Length; i++)
            {
                foreach (var j in nodes[i])
                {
                    graph.AddEdge(i, j);
                }
            }

            Assert.Equal(expected, graph.IsReachableFrom(src, dst));
        }
    }
}
