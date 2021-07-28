using System;
using System.Linq;
using System.Collections.Generic;

namespace Training.Common.Algorithms
{
    public class Knapsack
    {
        public static (int, int[]) BestKnapsack(int[] weights, int[] values, int w)
        {
            var knapsack = new HashSet<int>();
            var best = BestKnapsack(weights, values, w, weights.Length - 1, knapsack);
            return (best, knapsack.ToArray());
        }

        private static int BestKnapsack(int[] weights, int[] values, int w, int i, HashSet<int> knapsack)
        {
            if (i == -1)
            { return 0; }

            if (w == 0)
            { return 0; }

            if (w >= weights[i])
            {
                var include = BestKnapsack(weights, values, w - weights[i], i - 1, knapsack) + values[i];
                var exclude = BestKnapsack(weights, values, w, i - 1, knapsack);
                if (include > exclude)
                {
                    knapsack.Add(i);
                }
                return Math.Max(include, exclude);
            }
            else
            {
                return BestKnapsack(weights, values, w, i - 1, knapsack);
            }
        }

        public static (int, int[]) BestKnapsackMemo(int[] weights, int[] values, int w)
        {
            var n = weights.Length;
            var cache = new int[n][];
            for (var i = 0; i < cache.Length; i++)
            {
                cache[i] = new int[w];
            }
            for (var i = 0; i < cache.Length; i++)
            {
                for (var j = 0; j < cache[i].Length; j++)
                {
                    cache[i][j] = -1;
                }
            }
            var knapsack = new HashSet<int>();
            var best = BestKnapsackMemo(weights, values, w, n - 1, knapsack, cache);
            return (best, knapsack.ToArray());
        }

        private static int BestKnapsackMemo(int[] weights, int[] values, int w, int i, HashSet<int> knapsack, int[][] cache)
        {
            if (i == -1)
            { return 0; }
            if (w == 0)
            { return 0; }
            if (cache[i][w - 1] != -1)
            { return cache[i][w - 1]; }

            if (w >= weights[i])
            {
                var include = BestKnapsackMemo(weights, values, w - weights[i], i - 1, knapsack, cache) + values[i];
                var exclude = BestKnapsackMemo(weights, values, w, i - 1, knapsack, cache);
                if (include > exclude)
                {
                    knapsack.Add(i);
                }
                cache[i][w - 1] = Math.Max(include, exclude);
                return cache[i][w - 1];
            }
            else
            {
                cache[i][w - 1] = BestKnapsackMemo(weights, values, w, i - 1, knapsack, cache);
                return cache[i][w - 1];
            }
        }

        public static (int, int[]) BestKnapsackDp(int[] weights, int[] values, int W)
        {
            var N = weights.Length;
            var dp = new int[N + 1, W + 1];

            for (var i = 1; i <= N; i++)
            {
                for (var w = 1; w <= W; w++)
                {
                    if (weights[i - 1] <= w)
                    {
                        dp[i, w] = Math.Max(values[i - 1] + dp[i - 1, w - weights[i - 1]], dp[i - 1, w]);
                    }
                    else
                    {
                        dp[i, w] = dp[i - 1, w];
                    }
                }
            }

            var solution = new HashSet<int>();
            {
                var w = W;
                for (var i = N; i > 0; i--)
                {
                    if (weights[i - 1] <= w && values[i - 1] + dp[i - 1, w - weights[i - 1]] > dp[i - 1, w])
                    {
                        solution.Add(i - 1);
                        w -= weights[i - 1];
                    }
                }
            }

            return (dp[N, W], solution.ToArray());
        }
    }
}

