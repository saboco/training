using System;
using System.Collections.Generic;
using System.Linq;

namespace Training.Common.Algorithms
{
    public class LonguestSubsequece
    {
        public static int Longuest(int[] arr)
        {
            var max = 0;
            for (var i = 0; i < arr.Length; i++)
            {
                var sequences = new List<int[]>();
                var used = new HashSet<int>();
                Longuest(arr, i, Int32.MinValue, new List<int>(), sequences, used);
                max = Math.Max(max, sequences.Max(s => s.Length));
            }
            return max;
        }

        private static void Longuest(int[] arr, int index, int prev, List<int> sequence, List<int[]> sequences, HashSet<int> used)
        {
            if (index == arr.Length)
            {
                sequences.Add(sequence.ToArray());
            }

            for (var i = index; i < arr.Length; i++)
            {
                if (!used.Contains(arr[i]))
                {
                    used.Add(arr[i]);
                    if (prev > arr[i])
                    {
                        Longuest(arr, index + 1, prev, sequence, sequences, used);
                    }
                    else
                    {
                        sequence.Add(arr[i]);
                        Longuest(arr, index + 1, arr[i], sequence, sequences, used);
                        sequence.Remove(sequence.Count - 1);
                    }
                    used.Remove(arr[i]);
                }
            }
        }

        public static int LonguestIncreasingSubsequence(int[] arr)
        {
            return LonguestIncreasingSubsequence(arr, arr.Length - 1);

        }

        private static int LonguestIncreasingSubsequence(int[] arr, int i)
        {
            if (i == 0)
            { return 1; }

            var max = Int32.MinValue;
            for (var j = 0; j < i; j++)
            {
                if (arr[i] > arr[j])
                {
                    max = Math.Max(max, 1 + LonguestIncreasingSubsequence(arr, j));
                }
                else
                {
                    max = Math.Max(max, LonguestIncreasingSubsequence(arr, j));
                }
            }
            return max;
        }


        public static int LonguestMemo(int[] arr)
        {
            var cache = new int[arr.Length];
            return LonguestMemo(arr, arr.Length - 1, cache);
        }

        private static int LonguestMemo(int[] arr, int i, int[] cache)
        {
            if (i == 0)
            { return 1; }

            if (cache[i] != 0)
            {
                return cache[i];
            }

            var max = 1;
            for (var j = 0; j < i; j++)
            {
                if (arr[i] > arr[j])
                {
                    max = Math.Max(max, 1 + LonguestMemo(arr, j, cache));
                }
                else
                {
                    max = Math.Max(max, LonguestMemo(arr, j, cache));
                }
            }
            return cache[i] = max;
        }

        public static int LonguestDp(int[] arr)
        {
            var dp = new int[arr.Length];
            dp[0] = 1;
            for (var i = 1; i < arr.Length; i++)
            {
                for (var j = 0; j < i; j++)
                {
                    if (arr[i] > arr[j])
                    {
                        dp[i] = Math.Max(dp[i], 1 + dp[j]);
                    }
                    else
                    {
                        dp[i] = Math.Max(dp[i], dp[j]);
                    }
                }
            }
            return dp[arr.Length - 1];
        }
    }
}
