using System.Collections.Generic;

namespace Training.Leetcode
{
    public class TwoSumIISortedInput
    {
        public static int[] TwoSum(int[] numbers, int target)
        {
            var dic = new Dictionary<int, List<int>>();
            for (var i = 0; i < numbers.Length; i++)
            {
                if (!dic.ContainsKey(numbers[i]))
                {
                    dic.Add(numbers[i], new List<int>());
                }
                dic[numbers[i]].Add(i);
            }
            for (var i = 0; i < numbers.Length; i++)
            {
                var n = target - numbers[i];
                if (dic.ContainsKey(n))
                {
                    foreach (var j in dic[n])
                    {
                        if (j == i)
                        {
                            continue;
                        }

                        if (j > i)
                        {
                            return new[] { i + 1, j + 1 };
                        }
                        else
                        {
                            return new[] { j + 1, i + 1 };
                        }
                    }
                }
            }
            return new int[0];
        }

        public static int[] TwoSum2(int[] numbers, int target)
        {
            int lo = 0, hi = numbers.Length - 1;
            while (lo <= hi)
            {
                var sum = numbers[lo] + numbers[hi];
                if(sum == target)
                {
                    return new[]{lo+1,hi+1 };
                }
                else if(sum > target)
                {
                    hi--;
                }
                else
                { 
                    lo++;
                }
            }
            return new int[0];
        }
    }
}
