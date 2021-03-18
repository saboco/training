namespace Training.Leetcode
{
    public class RemoveLinkedListElements
    {
        public static ListNode RemoveElements(ListNode head, int val)
        {
            if (head == null)
            {
                return head;
            }

            while (head != null && head.val == val)
            {
                head = head.next;
            }

            var current = head;
            ListNode prev = null;
            while (current != null)
            {
                if (current.val == val)
                {
                    prev.next = current.next;
                }
                else
                {
                    prev = current;
                }
                current = current.next;
            }

            return head;
        }

        public static ListNode RemoveElementsWithSentinelNode(ListNode head, int val)
        {
            var sentinel = new ListNode(0)
            {
                next = head
            };

            var current = sentinel.next;
            var prev = sentinel;
            while (current != null)
            {
                if (current.val == val)
                {
                    prev.next = current.next;
                }
                else
                {
                    prev = current;
                }
                current = current.next;
            }

            return sentinel.next;
        }

        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }
        }
    }
}
