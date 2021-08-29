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

        public static (int, int[]) MinCost(int[][] costs)
        {
            if (costs.Length == 0)
            { return (0, Array.Empty<int>()); }

            var colorsRed = new List<int>();
            var colorsBlue = new List<int>();
            var colorsGreen = new List<int>();

            var redCost = MinCost(costs, 0, RED, colorsRed);
            var blueCost = MinCost(costs, 0, BLUE, colorsBlue);
            var greenCost = MinCost(costs, 0, GREEN, colorsGreen);

            if (redCost < blueCost && redCost < greenCost)
            {
                return (redCost, colorsRed.ToArray());
            }
            else if (blueCost < greenCost)
            {
                return (blueCost, colorsBlue.ToArray());
            }
            else
            {
                return (greenCost, colorsGreen.ToArray());
            }
        }


        private static int MinCost(int[][] costs, int house, int color, List<int> colors)
        {
            if (house == costs.Length)
            { return 0; }

            if (color == RED)
            {
                var blueColors = new List<int> { RED };
                var greenColors = new List<int> { RED };
                var blue = MinCost(costs, house + 1, BLUE, blueColors);
                var green = MinCost(costs, house + 1, GREEN, greenColors);
                if (blue < green)
                {
                    colors.AddRange(blueColors);
                }
                else
                {
                    colors.AddRange(greenColors);
                }
                return costs[house][RED] + Min(blue, green);
            }
            else if (color == BLUE)
            {
                var redColors = new List<int> { BLUE };
                var greenColors = new List<int> { BLUE };
                var red = MinCost(costs, house + 1, RED, redColors);
                var green = MinCost(costs, house + 1, GREEN, greenColors);
                if (red < green)
                {
                    colors.AddRange(redColors);
                }
                else
                {
                    colors.AddRange(greenColors);
                }
                return costs[house][BLUE] + Min(red, green);
            }
            else if (color == GREEN)
            {
                var redColors = new List<int> { GREEN };
                var blueColors = new List<int> { GREEN };
                var blue = MinCost(costs, house + 1, BLUE, blueColors);
                var red = MinCost(costs, house + 1, RED, redColors);
                if (red < blue)
                {
                    colors.AddRange(redColors);
                }
                else
                {
                    colors.AddRange(blueColors);
                }
                return costs[house][GREEN] + Min(red, blue);
            }

            return 0;
        }

        public static (int, int[]) MinCostMemo(int[][] costs)
        {

            if (costs.Length == 0)
            { return (0, Array.Empty<int>()); }

            var n = costs.Length;
            var cache = new int[n, 3];
            for (var i = 0; i < n; i++)
            {
                cache[i, 0] = -1;
                cache[i, 1] = -1;
                cache[i, 2] = -1;
            }
            
            var redCost = MinCostMemo(costs, 0, RED, cache);
            var blueCost = MinCostMemo(costs, 0, BLUE, cache);
            var greenCost = MinCostMemo(costs, 0, GREEN, cache);

            var colors = new int[n];
            var color = MinIndex((RED, cache[0, RED]), (BLUE, cache[0, BLUE]), (GREEN, cache[0, GREEN]));
            colors[0] = color;
            for (var i = 1; i < n; i++)
            {
                if (color == RED)
                {
                    color = MinIndex((BLUE, cache[i, BLUE]), (GREEN, cache[i, GREEN]));
                    colors[i] = color;
                }
                else if (color == BLUE)
                {
                    color = MinIndex((RED, cache[i, RED]), (GREEN, cache[i, GREEN]));
                    colors[i] = color;
                }
                else if (color == GREEN)
                {
                    color = MinIndex((BLUE, cache[i, BLUE]), (RED, cache[i, RED]));
                    colors[i] = color;
                }
            }

            var minCost = Min(redCost, blueCost, greenCost);
            return (minCost, colors.ToArray());
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

        public static (int, int[]) MinCostDp(int[][] costs)
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
            var minCost = Min(dp[N, RED], dp[N, BLUE], dp[N, GREEN]);
            var colors = new int[N];
            var color = MinIndex((RED, dp[N, RED]), (BLUE, dp[N, BLUE]), (GREEN, dp[N, GREEN]));
            colors[N - 1] = color;
            for (var i = N - 1; i > 0; i--)
            {
                if (color == RED)
                {
                    color = MinIndex((BLUE, dp[i, BLUE]), (GREEN, dp[i, GREEN]));
                    colors[i - 1] = color;
                }
                else if (color == BLUE)
                {
                    color = MinIndex((RED, dp[i, RED]), (GREEN, dp[i, GREEN]));
                    colors[i - 1] = color;
                }
                else if (color == GREEN)
                {
                    color = MinIndex((BLUE, dp[i, BLUE]), (RED, dp[i, RED]));
                    colors[i - 1] = color;
                }
            }

            return (minCost, colors);
        }

        private static int Min(int a, int b)
        {
            return Math.Min(a, b);
        }

        private static int Min(int a, int b, int c)
        {
            return Math.Min(a, Math.Min(b, c));
        }

        private static int MinIndex((int index, int value) a, (int index, int value) b)
        {
            if (a.value < b.value)
            {
                return a.index;
            }
            return b.index;
        }
        private static int MinIndex((int index, int value) a, (int index, int value) b, (int index, int value) c)
        {
            if (a.value < b.value && a.value < c.value)
            {
                return a.index;
            }
            else if (b.value < c.value)
            {
                return b.index;
            }
            return c.index;
        }

    }
}
