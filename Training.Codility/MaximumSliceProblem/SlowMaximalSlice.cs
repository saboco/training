using System;
using System.Text;

namespace Training.Codility.MaximumSliceProblem
{
    public class SlowMaximalSlice
    {
        public static int Solve(int[] a)
        {
            var sb = new StringBuilder();
            var n = a.Length;
            var result = 0;
            for (var i = 0; i < n; i++)
            {
                for (var j = i + 1; j < n; j++)
                {
                    var sum = 0;
                    sb.AppendLine("***********************************");
                    sb.AppendLine($"slice sum from {i} to {j}");
                    for (var k = i; k < j + 1; k++)
                    {
                        sum += a[k];
                        sb.AppendLine($"\tsum + {a[k]} = {sum}");
                    }
                    result = Math.Max(result, sum);
                    sb.AppendLine($"slice sum from {i} to {j} is {sum} and restul is {result}");
                    sb.AppendLine("***********************************");
                }
            }
            var s = sb.ToString();
            return result;
        }
    }
}