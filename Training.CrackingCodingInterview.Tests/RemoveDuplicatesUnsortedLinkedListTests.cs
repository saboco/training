using System;
using System.Collections.Generic;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class RemoveDuplicatesUnsortedLinkedListTests
    {
        List<Func<Node, Node>> _actions = new List<Func<Node, Node>>
        {
            //RemoveDuplicatesUnsortedLinkedList.RemoveDuplicates,
            //RemoveDuplicatesUnsortedLinkedList.RemoveDuplicates2,
            //RemoveDuplicatesUnsortedLinkedList.RemoveDuplicates3,
            //RemoveDuplicatesUnsortedLinkedList.RemoveDuplicatesInPlace,
            RemoveDuplicatesUnsortedLinkedList.RemoveDuplicatesInPlace2
        };
        [Theory]
        [InlineData(new[] { 5, 3, 5, 2, 1, 1 }, new[] { 5, 3, 2, 1 })]
        [InlineData(new[] { 1 }, new[] { 1 })]
        [InlineData(new[] { 1, 1 }, new[] { 1 })]
        [InlineData(new[] { 1, 1, 2, 2 }, new[] { 1, 2 })]
        [InlineData(new[] { 1, 1, 1, 1 }, new[] { 1 })]
        [InlineData(null, null)]
        [InlineData(new[] { 1, 2, 3, 4 }, new[] { 1, 2, 3, 4 })]
        [InlineData(new[] { 1, 2, 3, 4, 1, 1, 1 }, new[] { 1, 2, 3, 4 })]
        public void RemoveDuplicatesUnsortedLinkedListTest(int[] data, int[] expectedData)
        {
            foreach (var action in _actions)
            {
                var head = Node.Create(data);
                var actual = action.Invoke(head);
                var expected = Node.Create(expectedData);
                Assert.True(Node.AreEqual(expected, actual));
            }
        }

    }
}
