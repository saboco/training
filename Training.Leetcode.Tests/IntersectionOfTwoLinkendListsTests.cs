using System;
using Xunit;
using ListNode = Training.Leetcode.IntersectionOfTwoLinkedLists.ListNode;

namespace Training.Leetcode.Tests
{
    public class IntersectionOfTwoLinkendListsTests
    {
        private readonly Func<ListNode, ListNode, ListNode>[] _actions =
        {
            IntersectionOfTwoLinkedLists.GetIntersectionNode,
            IntersectionOfTwoLinkedLists.GetIntersectionNode2
        };

        [Theory]
        [InlineData(8, 2, 3, new[] { 4, 1, 8, 4, 5 }, new[] { 5, 0, 1, 8, 4, 5 })]
        [InlineData(2, 3, 1, new[] { 0, 9, 1, 2, 4 }, new[] { 3, 2, 4 })]
        [InlineData(-1, 3, 2, new[] { 2, 6, 4 }, new[] { 1, 5 })]
        [InlineData(-1, 1, 0, new[] { 1 }, new int[0])]
        public void IntersectionOfTwoLinkendListsTest(int expected, int skipA, int skipB, int[] listA, int[] listB)
        {
            ListNode headA = null;
            ListNode common = null;
            if (listA.Length > 0)
            {
                headA = new ListNode(listA[0]);
                var current = headA;
                for (var i = 1; i < listA.Length; i++)
                {
                    current.next = new ListNode(listA[i]);
                    if (skipA == i)
                    {
                        common = current.next;
                    }

                    current = current.next;
                }
            }
            ListNode headB = null;
            if (listB.Length > 0)
            {
                headB = new ListNode(listB[0]);

                var current = headB;
                for (var i = 1; i < listB.Length; i++)
                {
                    if (skipB == i)
                    {
                        current.next = common;
                    }
                    else
                    {
                        current.next = new ListNode(listB[i]);
                    }
                    current = current.next;
                }
            }
            foreach (var action in _actions)
            {
                var actual = action.Invoke(headA, headB);
                if (actual != null)
                { Assert.Equal(expected, actual.val); }
                else
                {
                    Assert.Equal(-1, expected);
                }
            }
        }
    }
}
