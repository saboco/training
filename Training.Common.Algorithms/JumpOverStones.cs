using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Common.Algorithms
{
    public class JumpOverStones
    {
        public static int MinCost(int[] cost, int x)
        {
            return MinCost(cost, x, cost.Length - 1);
        }

        private static int MinCost(int[] cost, int x, int i)
        {
            if (i < 0)
            { return 0; }
            var min = Int32.MaxValue;
            for (var j = 1; j <= x; j++)
            {
                min = Math.Min(min, MinCost(cost, x, i - j)) + cost[i];
            }
            return min;
        }

        public static int MinCostMemo(int[] cost, int x)
        {
            var cache = new int[cost.Length];
            return MinCostMemo(cost, x, cost.Length - 1, cache);
        }
        private static int MinCostMemo(int[] cost, int x, int i, int[] cache)
        {
            if (i < 0)
            { return 0; }
            if (cache[i] != 0)
            { return cache[i]; }
            var min = Int32.MaxValue;
            for (var j = 1; j <= x; j++)
            {
                min = Math.Min(min, MinCostMemo(cost, x, i - j, cache)) + cost[i];
            }
            return cache[i] = min;
        }

        public static int MinCostDp(int[] cost, int x)
        {
            var N = cost.Length;
            var dp = new int[N+1];
            for (var i = 1; i < dp.Length; i++)
            {
                var min = Int32.MaxValue;
                for (var j = 1; j <= x && i - j >= 0; j++)
                {
                    min = Math.Min(min, dp[i - j]);
                }
                dp[i] = min + cost[i-1];
            }
            return dp[N];
        }

        public static (int, int[]) MinCostDpWithPath(int[] cost, int x)
        {
            var N = cost.Length;
            var dp = new int[N+1];
            var path = new int[N+1];
            for (var i = 1; i < dp.Length; i++)
            {
                var min = Int32.MaxValue;
                var stone = -1;
                for (var j = 1; j <= x && i - j >= 0; j++)
                {
                    if(dp[i - j] < min)
                    { 
                        min = dp[i - j];
                        stone = i-j;
                    }
                }
                dp[i] = min + cost[i-1];
                path[i] = stone;
            }

            var stones = new List<int>();
            var k = path.Length -1;
            while(k > 0)
            { 
                stones.Add(k);
                k = path[k];
            }
            stones.Add(0); // you always start at 0
            stones.Reverse();
            return (dp[N], stones.ToArray());
        }
    }
}
