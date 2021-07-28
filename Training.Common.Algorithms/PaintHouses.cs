using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Common.Algorithms
{
    public class PaintHouses
    {
        private const int RED = 0;
        private const int BLUE = 1;
        private const int GREEN = 2;

        public static int MinCost(int[][] costs)
        {
            if (costs.Length == 0)
            { return 0; }

            return Min(
                   MinCost(costs, 0, RED),
                   MinCost(costs, 0, BLUE),
                   MinCost(costs, 0, GREEN));
        }


        private static int MinCost(int[][] costs, int house, int color)
        {
            if (house == costs.Length)
            { return 0; }

            if (color == RED)
            {
                var blue = MinCost(costs, house + 1, BLUE);
                var green = MinCost(costs, house + 1, GREEN);
                return costs[house][RED] + Min(blue, green);
            }
            else if (color == BLUE)
            {
                var red = MinCost(costs, house + 1, RED);
                var green = MinCost(costs, house + 1, GREEN);
                return costs[house][BLUE] + Min(red, green);
            }
            else if (color == GREEN)
            {
                var blue = MinCost(costs, house + 1, BLUE);
                var red = MinCost(costs, house + 1, RED);
                return costs[house][GREEN] + Min(red, blue);
            }

            return 0;
        }

        public static int MinCostMemo(int[][] costs)
        {

            if (costs.Length == 0)
            { return 0; }

            var n = costs.Length;
            var cache = new int[n, 3];
            for (var i = 0; i < n; i++)
            {
                cache[i, 0] = -1;
                cache[i, 1] = -1;
                cache[i, 2] = -1;
            }

            return Min(
                    MinCostMemo(costs, 0, RED, cache),
                    MinCostMemo(costs, 0, BLUE, cache),
                    MinCostMemo(costs, 0, GREEN, cache));

        }

        private static int MinCostMemo(int[][] costs, int house, int color, int[,] cache)
        {
            if (house == costs.Length)
            { return 0; }

            if (cache[house, color] != -1)
            {
                return cache[house, color];
            }

            if (color == RED)
            {
                var blue = MinCostMemo(costs, house + 1, BLUE, cache);
                var green = MinCostMemo(costs, house + 1, GREEN, cache);
                return cache[house, color] = costs[house][RED] + Min(blue, green);
            }
            else if (color == BLUE)
            {
                var red = MinCostMemo(costs, house + 1, RED, cache);
                var green = MinCostMemo(costs, house + 1, GREEN, cache);
                return cache[house, color] = costs[house][BLUE] + Min(red, green);
            }
            else if (color == GREEN)
            {
                var red = MinCostMemo(costs, house + 1, RED, cache);
                var blue = MinCostMemo(costs, house + 1, BLUE, cache);
                return cache[house, color] = costs[house][GREEN] + Min(red, blue);
            }
            return 0;
        }

        public static int MinCostDp(int[][] costs)
        {
            var N = costs.Length;
            var dp = new int[N + 1, 3];

            for (var i = 1; i <= N; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    dp[i, j] = costs[i - 1][j] + Min(dp[i - 1, (j + 1) % 3], dp[i - 1, (j + 2) % 3]);
                }

            }
            return Min(dp[N, RED], dp[N, BLUE], dp[N, GREEN]);
        }

        private static int Min(int a, int b)
        {
            return Math.Min(a, b);
        }

        private static int Min(int a, int b, int c)
        {
            return Math.Min(a, Math.Min(b, c));
        }
    }
}
