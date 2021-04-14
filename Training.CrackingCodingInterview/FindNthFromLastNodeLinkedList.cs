namespace Training.CrackingCodingInterview
{
    public class FindNthFromLastNodeLinkedList
    {
        public static Node Find(int nth, Node head)
        {
            if (head == null || nth < 0)
            { return head; }

            var n1 = head;
            var n2 = head;
            for (var i = 0; i < nth; i++)
            {
                if (n2 == null)
                { return null; }

                n2 = n2.Next;
            }
            while (n2.Next != null)
            {
                n1 = n1.Next;
                n2 = n2.Next;
            }

            return n1;
        }

        public static Node FindRecursive(int n, Node head)
        {
            if (head == null)
            { return null; }

            (_, Node nth) = FindRec(n, head);
            return nth;
        }

        private static (int count, Node nth) FindRec(int nth, Node node)
        {
            if (node.Next == null)
            {
                return (1, nth == 0 ? node : null);
            }

            (int count, Node n) = FindRec(nth, node.Next);

            if (count == nth)
            {
                n = node;
            }
            return (++count, n);
        }
    }
}
