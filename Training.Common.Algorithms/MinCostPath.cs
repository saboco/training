using System;
using System.Collections.Generic;
using System.Text;

namespace Training.Common.Algorithms
{
    public class MinCostPath
    {
        public static int MinCost(int[,] cost)
        {
            return MinCost(cost, cost.GetLength(0) - 1, cost.GetLength(1) - 1);
        }

        private static int MinCost(int[,] cost, int i, int j)
        {
            if (i == 0 && j == 0)
            { return cost[0, 0]; }

            var min = Int32.MaxValue;
            if (j - 1 >= 0)
            {
                min = Math.Min(min, MinCost(cost, i, j - 1));
            }
            if (i - 1 >= 0)
            {
                min = Math.Min(min, MinCost(cost, i - 1, j));
            }

            return min + cost[i, j];
        }

        public static int MinCostMemo(int[,] cost)
        {
            var cache = ArrayHelpers.NewMatrix(cost.GetLength(0), cost.GetLength(1), -1);
            return MinCostMemo(cost, cost.GetLength(0) - 1, cost.GetLength(1) - 1, cache);
        }

        private static int MinCostMemo(int[,] cost, int i, int j, int[,] cache)
        {
            if (i == 0 && j == 0)
            { return cost[0, 0]; }


            if (cache[i, j] != -1)
            {
                return cache[i, j];
            }
            var min = Int32.MaxValue;
            if (j - 1 >= 0)
            {
                min = Math.Min(min, MinCost(cost, i, j - 1));
            }
            if (i - 1 >= 0)
            {
                min = Math.Min(min, MinCost(cost, i - 1, j));
            }

            return cache[i, j] = min + cost[i, j];
        }

        public static int MinCostDp(int[,] cost)
        {
            var n = cost.GetLength(0);
            var m = cost.GetLength(1);
            var dp = new int[n, m];
            dp[0, 0] = cost[0, 0];
            for (var i = 0; i < dp.GetLength(0); i++)
            {
                for (var j = 0; j < dp.GetLength(1); j++)
                {
                    if (i == 0 && j == 0)
                    { continue; }

                    if (i == 0 && j > 0)
                    {
                        dp[i, j] = dp[i, j - 1];
                    }
                    else if (j == 0 && i > 0)
                    {
                        dp[i, j] = dp[i - 1, j];
                    }
                    else if (dp[i, j - 1] < dp[i - 1, j])
                    {
                        dp[i, j] = dp[i, j - 1];
                    }
                    else
                    {
                        dp[i, j] = dp[i - 1, j];
                    }
                    dp[i, j] += cost[i, j];
                }
            }
            return dp[n - 1, m - 1];
        }

        public static (int, string) MinCostDpPath(int[,] cost)
        {
            var n = cost.GetLength(0);
            var m = cost.GetLength(1);
            var dp = new int[n, m];
            dp[0, 0] = cost[0, 0];
            var path = new int[n, m];

            for (var i = 0; i < dp.GetLength(0); i++)
            {
                for (var j = 0; j < dp.GetLength(1); j++)
                {
                    if (i == 0 && j == 0)
                    { continue; }

                    if (i == 0 && j > 0)
                    {
                        dp[i, j] = dp[i, j - 1];
                        path[i, j] = 2;
                    }
                    else if (j == 0 && i > 0)
                    {
                        dp[i, j] = dp[i - 1, j];
                        path[i, j] = 1;
                    }
                    else
                    if (dp[i - 1, j] < dp[i, j - 1])
                    {
                        dp[i, j] = dp[i - 1, j];
                        path[i, j] = 1;
                    }
                    else
                    {
                        dp[i, j] = dp[i, j - 1];
                        path[i, j] = 2;
                    }
                    dp[i, j] += cost[i, j];
                }
            }
            var steps = new List<(int, int)>();
            int k = n - 1, l = m - 1;
            while (k > 0 || l > 0)
            {
                steps.Add((k, l));

                if (path[k, l] == 1)
                {
                    k--;
                }
                else if (path[k, l] == 2)
                {
                    l--;
                }
            }

            steps.Add((0, 0));
            var sb = new StringBuilder();
            for (var o = steps.Count - 1; o >= 0; o--)
            {
                var (i, j) = steps[o];
                sb.Append($"({i},{j})");
                if (o > 0)
                {
                    sb.Append($"->");
                }
            }

            return (dp[n - 1, m - 1], sb.ToString());

        }
    }
}
