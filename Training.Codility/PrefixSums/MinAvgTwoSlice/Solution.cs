using System;

namespace Training.Codility.PrefixSums.MinAvgTwoSlice
{
    public class Solution
    {
        public static int Solve(int[] a)
        {
            if (a.Length == 2)
            {
                return 0;
            }

            var p = PrefixSums.CalculatePrefixSums(a);
            var mean2 = CalculateMeanForEveryPair(p);
            var mean3 = CalculateMeanForEvery3(p);

            double min = p[p.Length - 1] < 0 ? 0 : p[p.Length - 1];
            var minIndex = -1;
            for (var i = 0; i < mean2.Length; i++)
            {
                if (!(mean2[i] < min))
                {
                    continue;
                }

                minIndex = i;
                min = mean2[i];
            }

            for (var i = 0; i < mean3.Length; i++)
            {
                if (!(mean3[i] < min))
                {
                    continue;
                }

                minIndex = i;
                min = mean3[i];
            }

            return minIndex;
        }

        private static double[] CalculateMeanForEvery3(int[] prefixSums)
        {
            return CalculateMeanForEveryN(prefixSums, 3);
        }

        private static double[] CalculateMeanForEveryPair(int[] prefixSums)
        {
            return CalculateMeanForEveryN(prefixSums, 2);
        }

        private static double[] CalculateMeanForEveryN(int[] prefixSums, int n)
        {
            var p = new double[prefixSums.Length - n];
            for (var i = 0; i < p.Length; i++)
            {
                p[i] = PrefixSums.CountTotal(prefixSums, i, i + n - 1) / (double)n;
            }
            return p;
        }
    }
}