using System;

namespace Training.Codility.MaximumSliceProblem.MaxSliceSum
{
    public class Solution
    {
        public static int Solve(int[] a)
        {
            if (a.Length == 0) return 0;

            long maxSlice = a[0];
            var maxEnding = 0L;

            for (var i = 0; i < a.Length; i++)
            {
                maxEnding = Math.Max(a[i], a[i] + maxEnding);
                maxSlice = Math.Max(maxSlice, maxEnding);
            }

            return (int) maxSlice;
        }
    }
}