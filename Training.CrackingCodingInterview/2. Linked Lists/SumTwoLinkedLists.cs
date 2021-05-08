namespace Training.CrackingCodingInterview
{
    public class SumTwoLinkedLists
    {
        public static Node Sum(Node n1, Node n2)
        {
            var rest = 0;
            var head = new Node(-1); // dummy
            var n = head;
            (int rest, int sum) Sum(int a, int b, int rest)
            {
                var sum = a + b + rest;
                return (sum > 9 ? 1 : 0, sum % 10);
            }
            while (n1 != null && n2 != null)
            {
                (int r, int sum) = Sum(n1.Data, n2.Data, rest);
                rest = r;
                n = n.AppendToTail(sum);
                n1 = n1.Next;
                n2 = n2.Next;
            }
            if (n1 == null)
            { n1 = n2; }
            while (n1 != null)
            {
                (int r, int sum) = Sum(n1.Data, 0, rest);
                rest = r;
                n = n.AppendToTail(sum);
                n1 = n1.Next;
            }
            if (rest > 0)
            {
                n.AppendToTail(rest);
            }
            return head.Next;
        }
    }
}
