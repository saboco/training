namespace Training.Common.Algorithms
{
    public class RegularExpression
    {
        public static bool Match(string s, string r)
        {
            return Match(s, r, s.Length - 1, r.Length - 1);
        }
        private static bool Match(string s, string r, int i, int j)
        {
            if ((i == -1 && j == -1) || (j == 0 && r[0] == '*'))
            { return true; }
            if (j == -1 || i == -1)
            { return false; }

            if (s[i] == r[j] || r[j] == '.')
            { return Match(s, r, i - 1, j - 1); }
            else if (r[j] == '*')
            {
                return Match(s, r, i - 1, j) || Match(s, r, i, j - 1);
            }
            return false;
        }

        public static bool MatchMemo(string s, string r)
        {
            var cache = ArrayHelpers.NewMatrix(s.Length + 1, r.Length + 1, -1);
            for (var i = 0; i <= s.Length; i++)
            { cache[i, 0] = 0; }
            for (var j = 0; j <= r.Length; j++)
            { cache[0, j] = 0; }

            return MatchMemo(s, r, s.Length - 1, r.Length - 1, cache);
        }
        private static bool MatchMemo(string s, string r, int i, int j, int[,] cache)
        {
            if ((i == -1 && j == -1) || (j == 0 && r[j] == '*'))
            {
                cache[i + 1, j + 1] = 1;
                return true;
            }
            if (j == -1 || i == -1)
            {
                cache[i + 1, j + 1] = 0;
                return false;
            }

            if (cache[i + 1, j + 1] != -1)
            { return cache[i, j] == 1; }

            if (s[i] == r[j] || r[j] == '.')
            {
                var m = MatchMemo(s, r, i - 1, j - 1, cache);
                cache[i + 1, j + 1] = m ? 1 : 0;
                return cache[i, j] == 1;
            }
            else if (r[j] == '*')
            {
                var match0 = MatchMemo(s, r, i, j - 1, cache);
                var match1 = MatchMemo(s, r, i - 1, j, cache);
                cache[i + 1, j + 1] = match0 || match1 ? 1 : 0;
                return cache[i + 1, j + 1] == 1;
            }
            cache[i + 1, j + 1] = 0;
            return false;
        }

        public static bool MatchDp(string s, string r)
        {
            var dp = ArrayHelpers.NewMatrix(s.Length + 1, r.Length + 1, -1);
            for (var i = 0; i <= s.Length; i++)
            { dp[i, 0] = 0; }
            for (var i = 0; i <= r.Length; i++)
            {
                dp[0, i] = 0;
                if (i - 1 >= 0)
                {
                    dp[0, i] = r[i - 1] == '*' ? 1 : 0;
                }
            }
            dp[0, 0] = 1; // s="" matches r=""

            for (var i = 1; i <= s.Length; i++)
            {
                for (var j = 1; j <= r.Length; j++)
                {
                    if (s[i - 1] == r[j - 1] || r[j - 1] == '.')
                    {
                        dp[i, j] = dp[i - 1, j - 1];
                    }
                    else if (r[j - 1] == '*')
                    {
                        dp[i, j] = dp[i, j - 1] == 1 || dp[i - 1, j] == 1 ? 1 : 0;
                    }
                    else
                    {
                        dp[i, j] = 0;
                    }

                }
            }
            return dp[s.Length, r.Length] == 1;
        }
    }
}
