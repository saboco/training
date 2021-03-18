using System.Collections.Generic;

namespace Training.Leetcode
{
    public class SingleNumber
    {
        public static int Single(int[] nums)
        {
            var count = new Dictionary<int, int>();
            for (var i = 0; i < nums.Length; i++)
            {
                if (!count.ContainsKey(nums[i]))
                {
                    count.Add(nums[i], 1);
                }
                else
                {
                    count[nums[i]]++;
                }
            }
            foreach (var kv in count)
            {
                if (kv.Value == 1)
                {
                    return kv.Key;
                }
            }
            return -1;
        }

        public static int SingleConstantMemory(int[] nums)
        {
            var single =0;
            for(var i=0; i < nums.Length; i++)
            { 
                single ^= nums[i];
            }
            return single;
        }
    }
}

