namespace Training.Common.Algorithms
{
    public class SumLeafNodes
    {   
        public static int Sum(Node node)
        {
            if (node == null)
            { return 0; }

            var sum = 0;
            foreach (var child in node.Children)
            {
                sum += Sum(child);
            }
            if (node.Children.Count == 0)
            {
                sum += node.Value;
            }
            return sum;
        }
    }
}
