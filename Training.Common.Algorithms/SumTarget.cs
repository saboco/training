using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Training.Common.Algorithms
{
    public class SumTarget
    {
        public static IEnumerable<int[]> GetSumsTarget(int[] arr, int target)
        {
            var sums = new Dictionary<string, int[]>();
            Array.Sort(arr);
            GetSumsTarget(arr, target, 0, new List<int>(), sums, 0);
            return sums.Select(kv => kv.Value);
        }

        private static void GetSumsTarget(int[] arr, int target, int currentSum, List<int> sum, Dictionary<string, int[]> sums, int k)
        {
            if (currentSum == target)
            {
                var key = GetKey(sum);
                sums[key] = sum.ToArray();
            }

            for (var i = k; i < arr.Length; i++)
            {
                var v = arr[i];
                if (v + currentSum > target)
                { continue; }

                currentSum = currentSum + v;
                sum.Add(v);
                GetSumsTarget(arr, target, currentSum, sum, sums, i + 1);
                currentSum = currentSum - v;
                sum.RemoveAt(sum.Count - 1);
            }
        }

        private static string GetKey(List<int> sum)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < sum.Count; i++)
            {
                sb.Append(sum[i]);
                if (i + 1 < sum.Count)
                {
                    sb.Append(',');
                }
            }
            return sb.ToString();
        }

        public static IEnumerable<int[]> GetSumsTargetSkipDuplicates(int[] arr, int target)
        {
            var sums = new List<int[]>();
            Array.Sort(arr);
            GetSumsTargetSkipDuplicates(arr, target, 0, new List<int>(), sums, 0);
            return sums;
        }

        private static void GetSumsTargetSkipDuplicates(int[] arr, int target, int currentSum, List<int> sum, List<int[]> sums, int k)
        {
            if (currentSum == target)
            { sums.Add(sum.ToArray()); }

            int? previous = null;
            for (var i = k; i < arr.Length; i++)
            {
                var v = arr[i];

                if (v + currentSum > target || previous == arr[i])
                { continue; }

                currentSum = currentSum + v;
                sum.Add(v);
                GetSumsTargetSkipDuplicates(arr, target, currentSum, sum, sums, i + 1);
                currentSum = currentSum - v;
                sum.RemoveAt(sum.Count - 1);
                previous = v;
            }
        }
    }
}
