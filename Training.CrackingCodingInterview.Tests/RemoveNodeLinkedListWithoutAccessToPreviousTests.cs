using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class RemoveNodeLinkedListWithoutAccessToPreviousTests
    {
        [Theory]
        [InlineData(2, new[] { 1, 2, 3, 4, 5 }, new[] { 1, 2, 4, 5 })]
        public void RemoveNode(int n, int[] data, int[] expectedData)
        {
            var actual = Node.Create(data);
            var node = actual;
            for (var i = 0; i < n; i++)
            {
                node = node.Next;
            }
            RemoveNodeLinkedListWithoutAccessToPrevious.RemoveNode(node);
            var expected = Node.Create(expectedData);
            Assert.True(Node.AreEqual(expected, actual));
        }
    }
}
