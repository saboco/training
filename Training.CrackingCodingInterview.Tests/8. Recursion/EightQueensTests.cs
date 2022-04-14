using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class EightQueensTests
    {
        [Fact]
        public void PositionQueensTest()
        {
            var validBoards = EightQueens.PrintHappyQueens();
            Assert.Equal(92, validBoards.Count);
        }
    }
}
