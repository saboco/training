using System.Collections.Generic;

namespace Training.Codility.CountingElements.PermCheck
{
    public class Solution
    {
        public static int Solve(int[] arr)
        {
            var dicNumberCount = new HashSet<int>();
            foreach (var i in arr)
            {
                if (!dicNumberCount.Contains(i))
                {
                    dicNumberCount.Add(i);
                }
                else
                {
                    return 0;
                }
            }
            for (var i = 1; i <= arr.Length; i++)
            {
                if (!dicNumberCount.Contains(i))
                {
                    return 0;
                }
            }
            return 1;
        }
    }
}