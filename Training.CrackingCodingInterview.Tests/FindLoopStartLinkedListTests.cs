using System;
using System.Collections.Generic;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class FindLoopStartLinkedListTests
    {
        List<Func<Node, Node>> _actions = new List<Func<Node, Node>>
        {
            //FindLoopStartLinkedList.FindLoopStart,
            FindLoopStartLinkedList.FindBeginning
        };

        [Theory]
        [InlineData(3, new[] { 1, 2, 3, 4, 5, 6 })]
        [InlineData(2, new[] { 8, 0, 2, 9, 4, 5, 6 })]
        [InlineData(0, new[] { 1 })]
        [InlineData(0, new[] { 1, 2 })]
        public void FindLoopStartTest(int n, int[] data)
        {
            foreach (var action in _actions)
            {
                var head = Node.Create(data);
                var expected = head;
                for (var i = 0; i < n; i++)
                {
                    expected = expected.Next;
                }
                var tail = expected;
                while (tail.Next != null)
                { tail = tail.Next; }
                tail.Next = expected;
                var actual = action.Invoke(head);
                Assert.Equal(expected, actual);
            }
        }
    }
}
