using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class MissingIntTests
    {
        [Theory]
        [InlineData(new[] { 0, 1, 2, 3, 4, 6, 7, 8, 9 }, 5)]
        [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 0)]
        [InlineData(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 10)]
        public void MissingIntTest(int[] arr, int expected)
        {
            var actual = MissingInt.Find(arr);
            Assert.Equal(expected, actual);
        }
    }
}
