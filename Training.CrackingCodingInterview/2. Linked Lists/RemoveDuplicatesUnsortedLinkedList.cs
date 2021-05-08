using System.Collections.Generic;

namespace Training.CrackingCodingInterview
{
    public class Node
    {
        public int Data { get; set; }
        public Node Next { get; set; }
        public Node(int d)
        {
            Data = d;
        }
        public Node AppendToTail(int d)
        {
            var end = new Node(d);
            var n = this;
            while (n.Next != null)
            { n = n.Next; }
            n.Next = end;
            return end;
        }
        public static Node Create(int[] data)
        {
            if (data == null)
            { return null; }
            var head = new Node(data[0]);
            var n = head;
            for (var i = 1; i < data.Length; i++)
            {
                n = n.AppendToTail(data[i]);
            }

            return head;
        }
        public static bool AreEqual(Node l1, Node l2)
        {
            if (l1 == l2)
            { return true; }
            if (l1 == null || l2 == null)
            { return false; }

            var n1 = l1;
            var n2 = l2;
            while (n1.Next != null && n2.Next != null)
            {
                if (n1.Data != n2.Data)
                { return false; }
                n1 = n1.Next;
                n2 = n2.Next;
            }
            return n1.Data == n2.Data;
        }
    }
    public class RemoveDuplicatesUnsortedLinkedList
    {
        public static Node RemoveDuplicates(Node head)
        {
            if (head == null)
            { return head; }

            var counter = new Dictionary<int, int>();
            var n = head;
            counter.Add(n.Data, 1);
            while (n?.Next != null)
            {
                var data = n.Next.Data;
                if (counter.ContainsKey(data))
                {
                    counter[data]++;
                }
                else
                {
                    counter.Add(data, 1);
                }
                n = n.Next;
            }
            n = head;
            while (n?.Next != null)
            {
                var data = n.Next.Data;
                if (counter[data] > 1)
                {
                    n.Next = n.Next.Next;
                    counter[data]--;
                }
                n = n.Next;
            }

            return head;
        }

        public static Node RemoveDuplicates2(Node head)
        {
            if (head == null)
            { return head; }

            var set = new HashSet<int>();
            set.Add(head.Data);
            var n = head;
            while (n?.Next != null)
            {
                if (set.Contains(n.Next.Data))
                { n.Next = n.Next.Next; }
                else
                { set.Add(n.Next.Data); }
                n = n.Next;
            }
            return head;
        }
        public static Node RemoveDuplicates3(Node head)
        {
            var set = new HashSet<int>();
            var n = head;
            Node previous = null;
            while (n != null)
            {
                if (set.Contains(n.Data))
                { previous.Next = n.Next; }
                else
                { set.Add(n.Data); }
                previous = n;
                n = n.Next;
            }
            return head;
        }

        public static Node RemoveDuplicatesInPlace(Node head)
        {
            if (head == null)
            { return head; }

            var ni = head;
            var nj = head;

            while (ni?.Next != null)
            {
                while (nj?.Next != null)
                {
                    if (ni.Data == nj.Next.Data)
                    {
                        nj.Next = nj.Next.Next;
                    }
                    nj = nj.Next;
                }
                ni = ni.Next;
            }
            return head;
        }

        public static Node RemoveDuplicatesInPlace2(Node head)
        {
            if (head == null)
            { return head; }

            var previous = head;
            var current = previous.Next;
            while (current != null)
            {
                var runner = head;
                while (runner != current)
                {
                    if (runner.Data == current.Data)
                    {
                        var tmp = current.Next;
                        previous.Next = tmp;
                        current = tmp;
                        break;
                    }
                    runner = runner.Next;
                }
                if (runner == current)
                {
                    previous = current;
                    current = current.Next;
                }
            }
            return head;
        }
    }
}
