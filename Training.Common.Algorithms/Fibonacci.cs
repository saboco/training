using System;

namespace Training.Common.Algorithms
{
    public static class Fibonacci
    {
        public static class Iterative
        {
            public static long GetFiboOf(long n)
            {
                if (n == 0L) return 0L;
                if (n == 1L) return 1L;

                var fib1 = 1L;
                var fib2 = 0L;
                for (var i = 2L; i < n; i++)
                {
                    var fibTemp = fib2 + fib1;
                    fib2 = fib1;
                    fib1 = fibTemp;
                }

                return fib1 + fib2;
            }
        }

        public static class Memoized
        {
            public static long GetFiboOf(long number)
            {
                Func<long, long> fibo = null;
                fibo = n =>
                {
                    if (n == 0) return 0;
                    if (n == 1) return 1;

                    return
                        // ReSharper disable once PossibleNullReferenceException
                        // ReSharper disable once AccessToModifiedClosure
                        fibo(n - 1)
                        +
                        // ReSharper disable once PossibleNullReferenceException
                        // ReSharper disable once AccessToModifiedClosure
                        fibo(n - 2);
                };
                fibo = fibo.Memoize();
                return fibo(number);
            }
        }

        /// <summary>
        /// Fibonacci declared recursively but as an iterative process
        /// </summary>
        public static class RecursiveIterative
        {
            public static long GetFiboOf(long n)
            {
                return Fibonacci(1, n, 0, 1);
            }

            private static long Fibonacci(long i, long n, long previousFibo, long current)
            {
                if (i >= n)
                {
                    return n == 0 ? 0 : current;
                }
                return Fibonacci(i + 1, n, current, current + previousFibo);
            }
        }

        /// <summary>
        /// Fibonacci declared recursively and as a recursive process
        /// </summary>
        public static class Recursive
        {
            public static long GetFiboOf(long n)
            {
                if (n == 0) return 0;
                if (n == 1) return 1;
                return GetFiboOf(n - 1) + GetFiboOf(n - 2);
            }
        }
    }
}