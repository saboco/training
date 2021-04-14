using Xunit;
using System;
using System.Collections.Generic;

namespace Training.CrackingCodingInterview.Tests
{
    public class FindNthFromLastNodeLinkedListTests
    {
        List<Func<int, Node, Node>> _actions = new List<Func<int, Node, Node>>
        {
            FindNthFromLastNodeLinkedList.Find,
            FindNthFromLastNodeLinkedList.FindRecursive
        };

        [Theory]
        [InlineData(1, new[] { 1, 2, 3, 4, 5 }, new[] { 4, 5 })]
        [InlineData(0, new[] { 1, 2, 3, 4, 5 }, new[] { 5 })]
        [InlineData(0, new[] { 1 }, new[] { 1 })]
        [InlineData(3, new[] { 0, 1, 3, 4 }, new[] { 0, 1, 3, 4 })]
        [InlineData(2, new[] { 0, 1, 3, 4 }, new[] { 1, 3, 4 })]
        [InlineData(2, new[] { 0, 1, 3, 4, 5, 6, 7, 8 }, new[] { 6, 7, 8 })]
        [InlineData(6, new[] { 0, 1, 2, 3, 4 }, null)]
        [InlineData(0, null, null)]
        public void FindNthFromLastNodeTest(int nth, int[] data, int[] expectedData)
        {
            foreach (var action in _actions)
            {
                var head = Node.Create(data);
                var actual = action.Invoke(nth, head);
                var expected = Node.Create(expectedData);
                Assert.True(Node.AreEqual(expected, actual));
            }
        }
    }
}
