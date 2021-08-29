using System;
using System.Collections.Generic;

namespace Training.Common.Algorithms
{
    public class RodCutting
    {
        public static (int, int[]) CutRod(int rod, int[] prices)
        {
            var cuts = new int[rod+1];
            var maxProfit = CutRod(rod, prices, cuts);
            var bestCuts = new List<int>();
            for(var i = rod; i > 0;)
            { 
                bestCuts.Add(cuts[i]);
                i-= cuts[i];
            }
            return (maxProfit, bestCuts.ToArray());
        }

        private static int CutRod(int rod, int[] prices, int[] cuts)
        {
            if (rod == 0)
            { return 0; }
            var max = Int32.MinValue;
            var bestCut = -1;
            for (var i = 0; i < rod; i++)
            {   
                var price = CutRod(rod - i - 1, prices, cuts);
                if (prices[i] + price > max)
                {
                    max = prices[i] + price;
                    bestCut = i+1;
                }
            }
            cuts[rod] = bestCut;
            return max;
        }

        public static (int, int[]) CutRodMemo(int rod, int[] prices)
        {
            var cache = new int[rod + 1];
            var cacheBestCut = new int[rod + 1];

            Array.Fill(cache, -1);
            var maxProfit = CutRodMemo(rod, prices, cache, cacheBestCut);
            var bestCuts = new List<int>();
            for(var i = rod; i > 0;)
            { 
                bestCuts.Add(cacheBestCut[i]);
                i-= cacheBestCut[i];
            }
            return (maxProfit, bestCuts.ToArray());
        }

        public static int CutRodMemo(int rod, int[] prices, int[] cache, int[] cacheBestCut)
        {
            if (rod == 0)
            { return 0; }

            if (cache[rod] != -1)
            {
                return cache[rod];
            }

            var maxProfit = Int32.MinValue;
            var bestCut = -1;
            for (var i = 0; i < rod; i++)
            {
                var cutProfit = CutRodMemo(rod - i - 1, prices, cache, cacheBestCut);
                if (prices[i] + cutProfit > maxProfit)
                {
                    maxProfit = prices[i] + cutProfit;
                    bestCut = i+1;
                }
            }
            cacheBestCut[rod] = bestCut;
            return cache[rod] = maxProfit;
        }

        public static (int, int[]) CutRodDp(int rod, int[] prices)
        {
            var dp = new int[rod + 1];
            var cuts = new int[rod + 1];

            for (var i = 1; i <= rod; i++)
            {
                dp[i] = Int32.MinValue;
                for (var j = 0; j < i; j++)
                {
                    if (prices[j] + dp[i - j - 1] > dp[i])
                    {
                        cuts[i] = j+1;
                        dp[i] = prices[j] + dp[i - j - 1];
                    }

                }
            }
            var bestCuts = new List<int>();
            for(var i =rod; i>0;)
            {
                bestCuts.Add(cuts[i]);
                i -= cuts[i];
            }
            return (dp[rod], bestCuts.ToArray());
        }
    }
}
