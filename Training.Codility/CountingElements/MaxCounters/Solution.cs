using System;

namespace Training.Codility.CountingElements.MaxCounters
{
    public class Solution
    {
        public static int[] Solve(int n, int[] operations)
        {
            var counters = new int[n];
            var max = 0;
            var lastMax = 0;
            Func<int, bool> isMaxCounters = i => i == n + 1;
            foreach (var operation in operations)
            {
                if (isMaxCounters(operation))
                {
                    lastMax = max;
                }
                else
                {
                    max = IncreaseCounter(operation - 1, counters, max, lastMax);
                }
            }
            SetCountersToLastMax(lastMax, counters);
            return counters;
        }

        private static int IncreaseCounter(int x, int[] counters, int max, int lastMax)
        {
            counters[x] = Math.Max(counters[x], lastMax) + 1;
            return counters[x] > max ? counters[x] : max;
        }

        private static void SetCountersToLastMax(int lastMax, int[] counters)
        {
            for (var i = 0; i < counters.Length; i++)
            {
                if (counters[i] < lastMax) counters[i] = lastMax;
            }
        }
    }
}