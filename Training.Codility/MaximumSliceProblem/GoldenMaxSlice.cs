using System;
using System.Text;

namespace Training.Codility.MaximumSliceProblem
{
    public class GoldenMaxSlice
    {
        public static int Solve(int[] a)
        {
            var maxSlice = 0;
            var maxEnding = 0;

            var sb = new StringBuilder();

            for (var i = 0; i < a.Length; i++)
            {
                maxEnding = Math.Max(0, a[i] + maxEnding);
                sb.AppendLine($"max ending is {maxEnding}");
                maxSlice = Math.Max(maxSlice, maxEnding);
                sb.AppendLine($"max slice is {maxSlice}");
            }
            var s = sb.ToString();
            return maxSlice;
        }
    }
}