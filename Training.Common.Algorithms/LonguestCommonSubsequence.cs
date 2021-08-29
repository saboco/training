using System;

namespace Training.Common.Algorithms
{
    public class LonguestCommonSubsequence
    {
        public static int Lcs(string a, string b)
        {
            return Lcs(a, b, a.Length - 1, b.Length - 1);
        }

        private static int Lcs(string a, string b, int i, int j)
        {
            if (i == -1 || j == -1)
            { return 0; }


            if (a[i] == b[j])
            {
                return Lcs(a, b, i - 1, j - 1) + 1;
            }
            else
            {
                return Math.Max(Lcs(a, b, i - 1, j), Lcs(a, b, i, j - 1));
            }
        }

        public static string LcsReconstruction(string a, string b)
        {
            return LcsReconstruction(a, b, a.Length - 1, b.Length - 1);
        }

        private static string LcsReconstruction(string a, string b, int i, int j)
        {
            if (i == -1 || j == -1)
            { return ""; }


            if (a[i] == b[j])
            {
                return LcsReconstruction(a, b, i - 1, j - 1) + a[i];
            }
            else
            {
                var s1 = LcsReconstruction(a, b, i - 1, j);
                var s2 = LcsReconstruction(a, b, i, j - 1);
                if (s1.Length > s2.Length)
                {
                    return s1;
                }
                else
                {
                    return s2;
                }
            }
        }

        public static int LcsMemo(string a, string b)
        {
            var cache = ArrayHelpers.NewMatrix(a.Length, b.Length, -1);
            return LcsMemo(a, b, a.Length - 1, b.Length - 1, cache);
        }

        private static int LcsMemo(string a, string b, int i, int j, int[,] cache)
        {
            if (i == -1 || j == -1)
            { return 0; }

            if (cache[i, j] != -1)
            { return cache[i, j]; }

            if (a[i] == b[j])
            {
                return cache[i, j] = LcsMemo(a, b, i - 1, j - 1, cache) + 1;
            }
            else
            {
                return cache[i, j] = Math.Max(LcsMemo(a, b, i - 1, j, cache), LcsMemo(a, b, i, j - 1, cache));
            }
        }

        public static string LcsMemoReconstruction(string a, string b)
        {
            var cache = ArrayHelpers.NewMatrix(a.Length, b.Length, "");
            return LcsMemoReconstruction(a, b, a.Length - 1, b.Length - 1, cache);
        }

        private static string LcsMemoReconstruction(string a, string b, int i, int j, string[,] cache)
        {
            if (i == -1 || j == -1)
            { return ""; }

            if (cache[i, j] != "")
            { return cache[i, j]; }

            if (a[i] == b[j])
            {
                return cache[i, j] = LcsMemoReconstruction(a, b, i - 1, j - 1, cache) + a[i];
            }
            else
            {
                var s1 = LcsMemoReconstruction(a, b, i - 1, j, cache);
                var s2 = LcsMemoReconstruction(a, b, i, j - 1, cache);
                if (s1.Length > s2.Length)
                {
                    return cache[i, j] = s1;
                }
                else
                {
                    return cache[i, j] = s2;
                }
            }
        }

        public static int LcsDp(string a, string b)
        {
            var dp = new int[a.Length + 1, b.Length + 1];

            for (var i = 1; i <= a.Length; i++)
            {
                for (var j = 1; j <= b.Length; j++)
                {
                    if (a[i - 1] == b[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                    }
                }
            }
            return dp[a.Length, b.Length];
        }

        public static string LcsDpReconstruction(string a, string b)
        {
            var dp = new string[a.Length + 1, b.Length + 1];
            for (var i = 0; i <= a.Length; i++)
            {
                for (var j = 0; j <= b.Length; j++)
                {
                    dp[i, j] = "";
                }
            }

            for (var i = 1; i <= a.Length; i++)
            {
                for (var j = 1; j <= b.Length; j++)
                {
                    if (a[i - 1] == b[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + a[i - 1];
                    }
                    else
                    {
                        if (dp[i - 1, j].Length > dp[i, j - 1].Length)
                        { dp[i, j] = dp[i - 1, j]; }
                        else
                        { dp[i, j] = dp[i, j - 1]; }
                    }
                }
            }
            return dp[a.Length, b.Length];
        }
    }
}
