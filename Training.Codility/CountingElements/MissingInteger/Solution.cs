using System.Collections.Generic;

namespace Training.Codility.CountingElements.MissingInteger
{
    public class Solution
    {
        public static int Solve(int[] arr)
        {
            var positiveElements = new HashSet<int>();
            foreach (var el in arr)
            {
                if (el > 0 && !positiveElements.Contains(el))
                {
                    positiveElements.Add(el);
                }
            }
            var i = 1;
            for (; i < positiveElements.Count + 1; i++)
            {
                if (!positiveElements.Contains(i))
                {
                    return i;
                }
            }
            return positiveElements.Count + 1;
        }
    }
}
