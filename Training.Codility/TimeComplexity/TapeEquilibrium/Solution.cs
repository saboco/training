using System;

namespace Training.Codility.TimeComplexity.TapeEquilibrium
{
    public class Solution
    {
        public static int Solve(int[] arr)
        {
            long rightSum = 0;
            if (arr.Length == 2) return Math.Abs(arr[1] - arr[0]);
            for (var i = 0; i < arr.Length; i++)
            {
                rightSum += arr[i];
            }
            var minDiff = long.MaxValue;
            var leftSum = 0;
            for (var i = 0; i < arr.Length - 1; i++)
            {
                leftSum += arr[i];
                rightSum -= arr[i];
                if (Math.Abs(rightSum - leftSum) < minDiff)
                {
                    minDiff = Math.Abs(rightSum - leftSum);
                }
            }
            return (int)minDiff;
        }
    }
}