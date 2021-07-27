using System;

namespace Training.Codility.Array.BinaryGap
{
    public class Solution
    {
        public int Solve(int n)
        {
            var binary = Convert.ToString(n, 2);
            var max = 0;
            var count = 0;
            foreach (var t in binary)
            {
                if (t == '1')
                {
                    max = Math.Max(max, count);
                    count = 0;
                }
                else
                {
                    count++;
                }
            }
            return max;
        }
    }
}