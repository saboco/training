using System.Collections.Generic;

namespace Training.Leetcode
{
    public class IntersectionOfTwoLinkedLists
    {
        public static ListNode GetIntersectionNode(ListNode headA, ListNode headB)
        {
            var set = new HashSet<ListNode>();
            var currentA = headA;
            while (currentA != null)
            {
                set.Add(currentA);
                currentA = currentA.next;
            }
            var currentB = headB;
            while (currentB != null)
            {
                if (set.Contains(currentB))
                {
                    return currentB;
                }
                currentB = currentB.next;
            }
            return null;
        }
        public static ListNode GetIntersectionNode2(ListNode headA, ListNode headB)
        {
            if (headA == null || headB == null)
            {
                return null;
            }

            var currentA = headA;
            var currentB = headB;
            var aJoinB = false;
            var bJoinA = false;
            while (true)
            {
                while (currentA != null && currentB != null)
                {
                    if (currentA == currentB)
                    {
                        return currentA;
                    }

                    currentA = currentA.next;
                    currentB = currentB.next;
                }
                if (currentA == null)
                {
                    if (aJoinB)
                    {
                        break;
                    }

                    currentA = headB;
                    aJoinB = true;
                }

                if (currentB == null)
                {
                    if (bJoinA)
                    {
                        break;
                    }

                    currentB = headA;
                    bJoinA = true;
                }
            }
            return null;
        }

        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }
        }
    }
}
