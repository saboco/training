using System;
using System.Diagnostics;
using Xunit;

namespace Training.Common.Algorithms.Tests
{    
    public class FibonacciTests
    {
        [Theory]
        [InlineData(0)]
        public void Should_return_0_when_n_is_0(long n)
        {
            Assert.Equal(0, Fibonacci.Iterative.GetFiboOf(n));
            Assert.Equal(0, Fibonacci.Memoized.GetFiboOf(n));
            Assert.Equal(0, Fibonacci.RecursiveIterative.GetFiboOf(n));
            Assert.Equal(0, Fibonacci.Recursive.GetFiboOf(n));
            Assert.Equal(0, Fibonacci.GoldenRatio.GetFiboOf(n));
            Assert.Equal(0, Fibonacci.DynamicProgramming.GetFibo(n));
        }

        [Theory]
        [InlineData(1)]
        public void Should_return_1_when_n_is_1(long n)
        {
            Assert.Equal(1, Fibonacci.Iterative.GetFiboOf(n));
            Assert.Equal(1, Fibonacci.Memoized.GetFiboOf(n));
            Assert.Equal(1, Fibonacci.RecursiveIterative.GetFiboOf(n));
            Assert.Equal(1, Fibonacci.Recursive.GetFiboOf(n));
            Assert.Equal(1, Fibonacci.GoldenRatio.GetFiboOf(n));
            Assert.Equal(1, Fibonacci.DynamicProgramming.GetFibo(n));
        }

        [Theory]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        [InlineData(5, 5)]
        [InlineData(6, 8)]
        public void Should_return_fib_of_n_less_1_plus_fib_of_n_less_2(long n, long fibo)
        {
            Assert.Equal(fibo, Fibonacci.Iterative.GetFiboOf(n));
            Assert.Equal(fibo, Fibonacci.Memoized.GetFiboOf(n));
            Assert.Equal(fibo, Fibonacci.RecursiveIterative.GetFiboOf(n));
            Assert.Equal(fibo, Fibonacci.Recursive.GetFiboOf(n));
            Assert.Equal(fibo, Fibonacci.GoldenRatio.GetFiboOf(n));
            Assert.Equal(fibo, Fibonacci.DynamicProgramming.GetFibo(n));
        }

        [Theory]
        [InlineData(40)]
        [InlineData(50)]
        [InlineData(1000)]
        public void Should_be_performant(int n)
        {
            var fiboImplementations = new System.Collections.Generic.List<Func<long, long>>
            {
                Fibonacci.Iterative.GetFiboOf,
                Fibonacci.Memoized.GetFiboOf,
                Fibonacci.RecursiveIterative.GetFiboOf,
                // Fibonacci.Recursive.GetFiboOf left out because it wont pass the test
            };
            var stopWatch = new Stopwatch();
            foreach (var fiboImplementation in fiboImplementations)
            {
                stopWatch.Start();
                fiboImplementation(n);
                stopWatch.Stop();
                Assert.InRange(stopWatch.ElapsedMilliseconds, 0, 20);
                stopWatch.Reset();
            }
        }
    }
}