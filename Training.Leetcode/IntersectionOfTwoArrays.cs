using System.Collections.Generic;
using System.Linq;

namespace Training.Leetcode
{
    public class IntersectionOfTwoArrays
    {
        public static int[] Intersection(int[] nums1, int[] nums2)
        {
            var set = new HashSet<int>();
            var result = new HashSet<int>();
            foreach (var i in nums1)
            {
                set.Add(i);
            }
            foreach (var i in nums2)
            {
                if (set.Contains(i))
                {
                    result.Add(i);
                }
            }

            return result.ToArray();
        }
    }
}
