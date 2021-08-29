using System;
using System.Collections.Generic;

namespace Training.Common.Algorithms
{
    public class EditDistance
    {
        public static int Min(string a, string b)
        {
            return Min(a, b, a.Length - 1, b.Length - 1);
        }

        private static int Min(string a, string b, int i, int j)
        {
            if (j == -1)
            { return i + 1; }
            if (i == -1)
            { return j + 1; }

            if (a[i] == b[j])
            {
                return Min(a, b, i - 1, j - 1);
            }
            else
            {
                return Math.Min(Min(a, b, i, j - 1),
                    Math.Min(Min(a, b, i - 1, j), Min(a, b, i - 1, j - 1))) + 1;
            }
        }

        public static (int, int)[] MinReconstruction(string a, string b)
        {
            var operations = new List<(int, int)>();
            var _ = MinReconstruction(a, b, a.Length - 1, b.Length - 1, operations);
            return operations.ToArray();
        }

        private enum Operation
        {
            Skip,
            Insert,
            Delete,
            Replace
        }

        private static int MinReconstruction(string a, string b, int i, int j, List<(int, int)> operations)
        {
            if (j == -1)
            {
                Add(operations, i + 1, (j, (int)Operation.Insert));
                return i + 1;
            }
            if (i == -1)
            {
                Add(operations, j + 1, (j, (int)Operation.Delete));
                return j + 1;
            }

            if (a[i] == b[j])
            {
                operations.Add((j, (int)Operation.Skip));
                return MinReconstruction(a, b, i - 1, j - 1, operations);
            }
            else
            {
                var deleteOperations = new List<(int, int)> { (j, (int)Operation.Delete) };
                var insertOperations = new List<(int, int)> { (j, (int)Operation.Insert) };
                var replaceOperations = new List<(int, int)> { (j, (int)Operation.Replace) };

                var delete = MinReconstruction(a, b, i, j - 1, deleteOperations) + 1;
                var insert = MinReconstruction(a, b, i - 1, j, insertOperations) + 1;
                var replace = MinReconstruction(a, b, i - 1, j - 1, replaceOperations) + 1;

                if (delete < insert && delete < replace)
                {
                    operations.AddRange(deleteOperations);
                    return delete;
                }
                else if (insert < replace)
                {
                    operations.AddRange(insertOperations);
                    return insert;
                }
                else
                {
                    operations.AddRange(replaceOperations);
                    return replace;
                }
            }
        }

        private static void Add(List<(int, int)> l, int n, (int, int) v)
        {
            for (var i = 0; i < n; i++)
            {
                l.Add(v);
            }
        }

        public static int MinMemo(string a, string b)
        {
            var cache = ArrayHelpers.NewMatrix(a.Length, b.Length, -1);
            return MinMemo(a, b, a.Length - 1, b.Length - 1, cache);
        }

        private static int MinMemo(string a, string b, int i, int j, int[,] cache)
        {
            if (j == -1)
            { return i + 1; }
            if (i == -1)
            { return j + 1; }

            if (cache[i, j] != -1)
            {
                return cache[i, j];
            }

            if (a[i] == b[j])
            {
                return cache[i, j] = MinMemo(a, b, i - 1, j - 1, cache);
            }
            else
            {
                return cache[i, j] = Math.Min(
                    MinMemo(a, b, i, j - 1, cache),
                    Math.Min(
                        MinMemo(a, b, i - 1, j, cache),
                        MinMemo(a, b, i - 1, j - 1, cache))) + 1;
            }
        }

        public static (int, int)[] MinMemoReconstruction(string a, string b)
        {
            var cache = ArrayHelpers.NewMatrix(a.Length, b.Length, -1);
            var decisions = ArrayHelpers.NewMatrix(a.Length + 1, b.Length + 1, -1);
            var operations = new List<(int, int)>();
            var _ = MinMemoReconstruction(a, b, a.Length - 1, b.Length - 1, cache, decisions);
            int i = a.Length, j = b.Length;
            while (i > 0 && j > 0)
            {
                switch ((Operation)decisions[i, j])
                {
                    case Operation.Skip:
                        operations.Add((j - 1, (int)Operation.Skip));
                        i--;
                        j--;
                        break;
                    case Operation.Insert:
                        operations.Add((j - 1, (int)Operation.Insert));
                        i--;
                        break;
                    case Operation.Delete:
                        operations.Add((j - 1, (int)Operation.Delete));
                        j--;
                        break;
                    case Operation.Replace:
                        operations.Add((j - 1, (int)Operation.Replace));
                        i--;
                        j--;
                        break;
                    default:
                        throw new InvalidOperationException("Unknown operation");
                }
            }
            if (i == 0)
            {
                var index = j - 1;
                while (j > 0)
                {
                    operations.Add((index, (int)Operation.Delete));
                    j--;
                }
            }
            if (j == 0)
            {
                while (i > 0)
                {
                    operations.Add((j - 1, (int)Operation.Insert));
                    i--;
                }
            }
            return operations.ToArray();
        }

        private static int MinMemoReconstruction(string a, string b, int i, int j, int[,] cache, int[,] decisions)
        {
            if (j == -1)
            {
                decisions[i + 1, j + 1] = (int)Operation.Insert;
                return i + 1;
            }
            if (i == -1)
            {
                decisions[i + 1, j + 1] = (int)Operation.Delete;
                return j + 1;
            }

            if (cache[i, j] != -1)
            {
                return cache[i, j];
            }

            if (a[i] == b[j])
            {
                decisions[i + 1, j + 1] = (int)Operation.Skip;
                return cache[i, j] = MinMemoReconstruction(a, b, i - 1, j - 1, cache, decisions);
            }
            else
            {
                var delete = MinMemoReconstruction(a, b, i, j - 1, cache, decisions) + 1;
                var insert = MinMemoReconstruction(a, b, i - 1, j, cache, decisions) + 1;
                var replace = MinMemoReconstruction(a, b, i - 1, j - 1, cache, decisions) + 1;

                if (delete < insert && delete < replace)
                {
                    decisions[i + 1, j + 1] = (int)Operation.Delete;
                    return cache[i, j] = delete;
                }
                else if (insert < replace)
                {
                    decisions[i + 1, j + 1] = (int)Operation.Insert;
                    return cache[i, j] = insert;
                }
                else
                {
                    decisions[i + 1, j + 1] = (int)Operation.Replace;
                    return cache[i, j] = replace;
                }
            }
        }

        public static int MinDp(string a, string b)
        {
            var dp = new int[a.Length + 1, b.Length + 1];
            for (var i = 0; i <= a.Length; i++)
            {
                dp[i, 0] = i;
            }
            for (var i = 0; i <= b.Length; i++)
            {
                dp[0, i] = i;
            }

            dp[0, 0] = 0;
            for (var i = 1; i <= a.Length; i++)
            {
                for (var j = 1; j <= b.Length; j++)
                {
                    if (a[i - 1] == b[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1];
                    }
                    else
                    {
                        dp[i, j] =
                            Math.Min(dp[i - 1, j],
                                Math.Min(dp[i, j - 1], dp[i - 1, j - 1])) + 1;
                    }
                }
            }
            return dp[a.Length, b.Length];
        }

        public static (int, int)[] MinDpReconstruction(string a, string b)
        {
            var dp = new int[a.Length + 1, b.Length + 1];
            var decisions = new int[a.Length + 1, b.Length + 1];

            for (var i = 0; i <= a.Length; i++)
            {
                dp[i, 0] = i;
            }
            for (var i = 0; i <= b.Length; i++)
            {
                dp[0, i] = i;
            }

            dp[0, 0] = 0;
            for (var i = 1; i <= a.Length; i++)
            {
                for (var j = 1; j <= b.Length; j++)
                {
                    if (a[i - 1] == b[j - 1])
                    {
                        decisions[i, j] = (int)Operation.Skip;
                        dp[i, j] = dp[i - 1, j - 1];
                    }
                    else
                    {
                        if (dp[i - 1, j] < dp[i, j - 1] && dp[i - 1, j] < dp[i - 1, j - 1])
                        {
                            decisions[i, j] = (int)Operation.Insert;
                            dp[i, j] = dp[i - 1, j];
                        }
                        else if (dp[i, j - 1] < dp[i - 1, j - 1])
                        {
                            decisions[i, j] = (int)Operation.Delete;
                            dp[i, j] = dp[i, j - 1];
                        }
                        else
                        {
                            decisions[i, j] = (int)Operation.Replace;
                            dp[i, j] = dp[i - 1, j - 1];
                        }
                        dp[i, j] += 1;
                    }
                }
            }

            var operations = new List<(int, int)>();
            {
                int i = a.Length, j = b.Length;
                while (i > 0 && j > 0)
                {
                    switch ((Operation)decisions[i, j])
                    {
                        case Operation.Skip:
                            operations.Add((j - 1, (int)Operation.Skip));
                            i--;
                            j--;
                            break;
                        case Operation.Insert:
                            operations.Add((j - 1, (int)Operation.Insert));
                            i--;
                            break;
                        case Operation.Delete:
                            operations.Add((j - 1, (int)Operation.Delete));
                            j--;
                            break;
                        case Operation.Replace:
                            operations.Add((j - 1, (int)Operation.Replace));
                            i--;
                            j--;
                            break;
                        default:
                            throw new InvalidOperationException("Unknown operation");
                    }
                }
                if (i == 0)
                {
                    var index = j - 1;
                    while (j > 0)
                    {
                        operations.Add((index, (int)Operation.Delete));
                        j--;
                    }
                }
                if (j == 0)
                {
                    while (i > 0)
                    {
                        operations.Add((j - 1, (int)Operation.Insert));
                        i--;
                    }
                }
            }
            return operations.ToArray();
        }
    }
}
