using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class SumTwoLinkedListsTests
    {
        [Theory]
        [InlineData(new[] { 3, 1, 5 }, new[] { 5, 9, 2 }, new[] { 8, 0, 8 })]
        [InlineData(new[] { 0, 0, 1 }, new[] { 1 }, new[] { 1, 0, 1 })]
        [InlineData(new[] { 0, 0, 1 }, new[] { 0 }, new[] { 0, 0, 1 })]
        [InlineData(new[] { 1 }, new[] { 0, 0, 1 }, new[] { 1, 0, 1 })]
        [InlineData(new[] { 0 }, new[] { 0, 0, 1 }, new[] { 0, 0, 1 })]
        [InlineData(new[] { 0 }, new[] { 0 }, new[] { 0 })]
        [InlineData(new[] { 0 }, null, new[] { 0 })]
        [InlineData(null, new[] { 0 }, new[] { 0 })]
        [InlineData(null, null, null)]
        [InlineData(new[] { 9, 9, 9 }, new[] { 9, 9 }, new[] { 8, 9, 0, 1 })]
        [InlineData(new[] { 9, 9, 9 }, new[] { 9, 9, 9 }, new[] { 8, 9, 9, 1 })]
        [InlineData(new[] { 9, 8, 9 }, new[] { 9, 0, 9 }, new[] { 8, 9, 8, 1 })]
        public void SumTwoLinkedListsTest(int[] n1Data, int[] n2Data, int[] expectedData)
        {
            var n1 = Node.Create(n1Data);
            var n2 = Node.Create(n2Data);
            var actual = SumTwoLinkedLists.Sum(n1, n2);
            var expected = Node.Create(expectedData);
            Assert.True(Node.AreEqual(expected, actual));
        }
    }
}
