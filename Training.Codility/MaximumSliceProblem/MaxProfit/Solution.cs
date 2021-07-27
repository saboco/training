using System;

namespace Training.Codility.MaximumSliceProblem.MaxProfit
{
    public class Solution
    {
        public static int Solve(int[] prices)
        {
            var maxProfit = 0;
            var n = prices.Length;

            if (prices.Length == 0)
            {
                return maxProfit;
            }

            var minPrice = prices[0];

            for (var i = 0; i < n; i++)
            {
                minPrice = Math.Min(minPrice, prices[i]);
                maxProfit = Math.Max(maxProfit, prices[i] - minPrice);
            }

            return maxProfit;
        }
    }
}