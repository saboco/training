namespace Training.Common.Algorithms
{
    public class FloydWarshall
    {
        public static double[,] AllPairsShortestPath(double[,] g)
        {
            var n = g.GetLength(0);
            var dp = new double[n, n];
            var next = new int[n, n];

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    dp[i, j] = g[i, j];
                    if (g[i, j] != double.PositiveInfinity)
                    {
                        next[i, j] = j;
                    }
                }
            }

            for (var k = 0; k < n; k++)
            {
                for (var i = 0; i < n; i++)
                {
                    for (var j = 0; j < n; j++)
                    {
                        if (dp[i, j] > dp[i, k] + dp[k, j])
                        {
                            dp[i, j] = dp[i, k] + dp[k, j];
                            next[i, j] = next[i,k];
                        }
                    }
                }
            }

            for (var k = 0; k < n; k++)
            {
                for (var i = 0; i < n; i++)
                {
                    for (var j = 0; j < n; j++)
                    {
                        if (dp[i, j] > dp[i, k] + dp[k, j])
                        {
                            dp[i, j] = double.NegativeInfinity;
                            next[i, j] = -1;
                        }
                    }
                }
            }

            return dp;
        }
    }
}
