using System;

namespace Training.Codility.MaximumSliceProblem.MaxDoubleSliceSum
{
    public class Solution
    {
        public static int Solve(int[] a)
        {
            var n = a.Length - 2;
            var leftSlices = new int[n];
            var rightSlices = new int[n];

            for (int i = 0, to = n - 1; i < to; i++)
            {
                leftSlices[i + 1] = Math.Max(0, leftSlices[i] + a[i + 1]);
                rightSlices[to - i - 1] = Math.Max(0, rightSlices[to - i] + a[to - i + 1]);
            }

            var maxDoubleSlice = 0;
            for (var i = 0; i < n; i++)
            {
                maxDoubleSlice = Math.Max(maxDoubleSlice, rightSlices[i] + leftSlices[i]);
            }

            return maxDoubleSlice;
        }
    }
}