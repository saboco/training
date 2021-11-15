using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class FloydWarshallTests
    {
        [Fact]
        public void AllPairsShortestPathTest()
        {
            var n = 7;
            var graph = Common.CreateEmptyAdjacencyMatrix(n);
            var expected = Common.CreateEmptyAdjacencyMatrix(n);

            graph[0, 1] = 2;
            graph[0, 2] = 5;
            graph[0, 6] = 10;
            graph[1, 2] = 2;
            graph[1, 4] = 11;
            graph[2, 6] = 2;
            graph[6, 5] = 11;
            graph[4, 5] = 1;
            graph[5, 4] = -2;
                        
            expected[0,0] = 0;
            expected[0,1] = 2;
            expected[0,2] = 4;
            expected[0,3] = double.PositiveInfinity;
            expected[0,4] = double.NegativeInfinity;
            expected[0,5] = double.NegativeInfinity;
            expected[0,6] = 6;

            expected[1,0] = double.PositiveInfinity;
            expected[1,1] = 0;
            expected[1,2] = 2;
            expected[1,3] = double.PositiveInfinity;
            expected[1,4] = double.NegativeInfinity;
            expected[1,5] = double.NegativeInfinity;
            expected[1,6] = 4;

            expected[2,0] = double.PositiveInfinity;
            expected[2,1] = double.PositiveInfinity;
            expected[2,2] = 0;
            expected[2,3] = double.PositiveInfinity;
            expected[2,4] = double.NegativeInfinity;
            expected[2,5] = double.NegativeInfinity;
            expected[2,6] = 2;

            expected[3,0] = double.PositiveInfinity;
            expected[3,1] = double.PositiveInfinity;
            expected[3,2] = double.PositiveInfinity;
            expected[3,3] = 0;
            expected[3,4] = double.PositiveInfinity;
            expected[3,5] = double.PositiveInfinity;
            expected[3,6] = double.PositiveInfinity;

            expected[4,0] = double.PositiveInfinity;
            expected[4,1] = double.PositiveInfinity;
            expected[4,2] = double.PositiveInfinity;
            expected[4,3] = double.PositiveInfinity;
            expected[4,4] = double.NegativeInfinity;
            expected[4,5] = double.NegativeInfinity;
            expected[4,6] = double.PositiveInfinity;

            expected[5,0] = double.PositiveInfinity;
            expected[5,1] = double.PositiveInfinity;
            expected[5,2] = double.PositiveInfinity;
            expected[5,3] = double.PositiveInfinity;
            expected[5,4] = double.NegativeInfinity;
            expected[5,5] = double.NegativeInfinity;
            expected[5,6] = double.PositiveInfinity;

            expected[6,0] = double.PositiveInfinity;
            expected[6,1] = double.PositiveInfinity;
            expected[6,2] = double.PositiveInfinity;
            expected[6,3] = double.PositiveInfinity;
            expected[6,4] = double.NegativeInfinity;
            expected[6,5] = double.NegativeInfinity;
            expected[6,6] = 0;


            var distances = FloydWarshall.AllPairsShortestPath(graph);
            Common.AssertEqual(expected, distances);
        }
    }
}
