using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Training.Leetcode.Tests
{
    public class RemoveLinkedListElementsTests
    {
        private readonly Func<RemoveLinkedListElements.ListNode, int, RemoveLinkedListElements.ListNode>[] _actions =
        {
            RemoveLinkedListElements.RemoveElements,
            RemoveLinkedListElements.RemoveElementsWithSentinelNode
        };

        [Theory]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, new[] { 1, 2, 6, 3, 4, 5, 6 }, 6)]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, new[] { 1, 2, 6, 6, 3, 4, 5, 6 }, 6)]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, new[] { 1, 2, 6, 6, 3, 4, 5, 6, 6, 6 }, 6)]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, new[] { 6, 6, 6, 1, 2, 6, 6, 3, 4, 5, 6, 6, 6 }, 6)]
        [InlineData(new int[0], new[] { 6, 6, 6 }, 6)]
        [InlineData(new int[0], new[] { 6 }, 6)]
        [InlineData(new int[0], new int[0], 6)]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, new[] { 0, 1, 2, 0, 3, 4, 5, 0 }, 0)]
        public void RemoveLinkedListElementsTest(int[] expected, int[] list, int val)
        {
            foreach (var action in _actions)
            {
                var head = list.Length == 0
                    ? null
                    : new RemoveLinkedListElements.ListNode(list[0]);

                var current = head;
                for (var i = 1; i < list.Length; i++)
                {
                    current.next = new RemoveLinkedListElements.ListNode(list[i]);
                    current = current.next;
                }
                head = action.Invoke(head, val);
                current = head;
                var j = 0;
                while (current != null && j < expected.Length)
                {
                    Assert.Equal(expected[j++], current.val);
                    current = current.next;
                }
                Assert.Null(current);
            }
        }
    }
}
