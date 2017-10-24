using System.Collections.Generic;

namespace Training.HackerRank.Techniques_Concepts
{
    public static class DynamicProgrammingCoinChange
    {
        public static long GetWaysOfMakeChange(int amount, int[] coins, Dictionary<string, long> memo)
        {
            if (amount == 0) return 1;
            if (amount < 0) return 0;
            if (coins.Length == 0) return 0;

            var key = string.Concat(amount, "¤", string.Join("¤", coins));
            if (memo.ContainsKey(key)) return memo[key];

            var result = GetWaysOfMakeChange(amount - coins[0], coins, memo)
                         + GetWaysOfMakeChange(amount, GetCoinsWithoutFirstKind(coins), memo);

            memo.Add(key, result);
            return memo[key];
        }

        private static int[] GetCoinsWithoutFirstKind(int[] coins)
        {
            var newCoins = new int[coins.Length - 1];
            for (var i = 0; i < newCoins.Length; i++)
            {
                newCoins[i] = coins[i + 1];
            }
            return newCoins;
        }
    }
}