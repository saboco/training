using System;

namespace Training.Leetcode
{
    public class BestTimeToBuyAndSellStockII
    {
        public static int MaxProfit(int[] prices)
        {
            /* we have 3 possible decisions 
             * * Buy
             * * Sell (only if we have bought)
             * * Do nothing
             * 
             * We have to try all posibilities
             * 
             * We only have a profit if we can sell and the sell - buy is positif
             */
            return DP(-1, 0, 0, prices);
        }

        private static int DP(int bought, int profit, int i, int[] prices)
        {
            if (i > prices.Length - 1)
            { return profit; }

            if (bought >= prices[i])
            {
                return 0;
            }

            if (bought == -1)
            {
                return Math.Max(
                    DP(-1, profit, i + 1, prices),
                    DP(prices[i], profit, i + 1, prices));
            }
            else
            {
                return Math.Max(
                    DP(bought, profit, i + 1, prices),
                    DP(-1, Math.Max(0, profit + (prices[i] - bought)), i + 1, prices));
            }
        }

        public static int MaxProfitGreddy(int[] prices)
        {   
            var profit = 0;
            var currentPrice = prices[0];
            for (var i = 1; i < prices.Length; i++)
            {
                if (prices[i] - currentPrice > 0)
                {
                    profit += prices[i] - currentPrice;
                }

                currentPrice = prices[i];
            }
            return profit;
        }
    }
}
