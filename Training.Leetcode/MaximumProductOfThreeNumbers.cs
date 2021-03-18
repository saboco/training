using System;

namespace Training.Leetcode
{
    public class MaximumProductOfThreeNumbers
    {
        public static int MaximumProduct(int[] nums)
        {
            Array.Sort(nums);

            if (nums[0] >= 0 || nums[nums.Length - 1] < 0)
            {
                return
                    nums[nums.Length - 1] *
                    nums[nums.Length - 2] *
                    nums[nums.Length - 3];
            }
            else if (nums[nums.Length - 1] == 0)
            {
                return 0;
            }
            else if (nums[0] * nums[1] * nums[nums.Length - 1] > nums[nums.Length - 1] *
                   nums[nums.Length - 2] *
                   nums[nums.Length - 3])
            {
                return nums[0] * nums[1] * nums[nums.Length - 1];
            }

            return nums[nums.Length - 1] *
                    nums[nums.Length - 2] *
                    nums[nums.Length - 3];
        }

        public static int MaximumProduct2(int[] nums)
        {
            Array.Sort(nums);
            return Math.Max(
                nums[0] * nums[1] * nums[nums.Length - 1],
                nums[nums.Length - 1] * nums[nums.Length - 2] * nums[nums.Length - 3]);
        }

        public static int MaximumProduct3(int[] nums)
        {
            int min1 = 1001, min2 = 1001;
            int max1 = -1001, max2 = -1001, max3 = -1001;

            for (var i = 0; i < nums.Length; i++)
            {
                if (nums[i] < min1)
                {
                    min2 = min1;
                    min1 = nums[i];
                }
                else if (nums[i] < min2)
                {
                    min2 = nums[i];
                }
                if (nums[i] > max1)
                {
                    max3 = max2;
                    max2 = max1;
                    max1 = nums[i];
                }
                else if (nums[i] > max2)
                {
                    max3 = max2;
                    max2 = nums[i];
                }
                else if (nums[i] > max3)
                {
                    max3 = nums[i];
                }
            }

            return Math.Max(
                min1 * min2 * max1,
                max1 * max2 * max3);
        }
    }
}
