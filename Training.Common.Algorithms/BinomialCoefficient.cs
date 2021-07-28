namespace Training.Common.Algorithms
{
    public class BinomialCoefficient
    {
        public static int C(int n, int k)
        {
            if (k == 0 || k == n)
            { return 1; }
            return C(n - 1, k - 1) + C(n - 1, k);
        }

        public static int Cmemo(int n, int k)
        {
            var cache = new int[n + 1, k + 1];
            return Cmemo(n, k, cache);
        }

        private static int Cmemo(int n, int k, int[,] cache)
        {
            if (k == 0 || k == n)
            { return 1; }

            if (cache[n, k] != 0)
            {
                return cache[n, k];
            }

            cache[n, k] = C(n - 1, k - 1) + C(n - 1, k);

            return cache[n, k];
        }

        public static int Cdp(int n, int k)
        {
            var dp = new int[n + 1, k + 1];
            for (var i = 0; i <= n; i++)
            {
                dp[i, 0] = 1;
                if (i <= k)
                { dp[i, i] = 1; }
            }

            for (var i = 1; i <= n; i++)
            {
                for (var j = 1; j <= k; j++)
                {
                    dp[i, j] = dp[i - 1, j - 1] + dp[i - 1, j];
                }
            }
            return dp[n, k];
        }
    }
}
