namespace Training.CrackingCodingInterview
{
    public class FindNthNodeLinkedList
    {
        public static Node Find(int nth, Node head)
        {
            var node = head;
            for (var i = 0; i < nth; i++)
            {
                node = node.Next;
            }
            return node;
        }
    }
}
