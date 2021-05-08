using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.CrackingCodingInterview
{
    public class FindLoopStartLinkedList
    {
        public static Node FindLoopStart(Node head)
        {
            var set = new HashSet<Node>();
            var n = head;
            while (n != null)
            {
                if (!set.Add(n))
                { return n; }
                n = n.Next;
            }
            return null; //no loop
        }

        public static Node FindBeginning(Node head)
        {
            var n1 = head;
            var n2 = head;
            while (n2.Next != null)
            {   
                n1 = n1.Next;
                n2 = n2.Next.Next;
                if (n1 == n2)
                { break; }
            }

            if (n2 == null)// no loop
            { return null; }

            n2 = head;
            while (n1 != n2)
            {
                n1 = n1.Next;
                n2 = n2.Next;
            }
            return n2;
        }
    }
}
