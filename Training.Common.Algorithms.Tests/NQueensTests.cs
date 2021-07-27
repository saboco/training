using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class NQueensTests
    {

        [Theory]
        [InlineData(1, true)]
        [InlineData(2, false)]
        [InlineData(3, false)]
        [InlineData(4, true)]
        [InlineData(5, true)]
        [InlineData(6, true)]
        [InlineData(7, true)]
        [InlineData(8, true)]
        public void NQueenTest(int n, bool expected)
        {
            Assert.Equal(expected, NQueens.SolveNQueens(n));
        }
    }
}
