namespace Training.CrackingCodingInterview
{
    public class RemoveNodeLinkedListWithoutAccessToPrevious
    {
        public static void RemoveNode(Node n)
        {   
            n.Data = n.Next.Data;
            n.Next=n.Next.Next;
        }
    }
}
