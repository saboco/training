using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Training.Common.Algorithms.Tests
{
    [TestFixture]
    public class FibonacciTests
    {
        [TestCase(0)]
        public void Should_return_0_when_n_is_0(long n)
        {
            Assert.AreEqual(0, Fibonacci.Iterative.GetFiboOf(n));
            Assert.AreEqual(0, Fibonacci.Memoized.GetFiboOf(n));
            Assert.AreEqual(0, Fibonacci.RecursiveIterative.GetFiboOf(n));
            Assert.AreEqual(0, Fibonacci.Recursive.GetFiboOf(n));
            Assert.AreEqual(0, Fibonacci.GoldenRatio.GetFiboOf(n));
            Assert.AreEqual(0, Fibonacci.DynamicProgramming.GetFibo(n));
        }

        [TestCase(1)]
        public void Should_return_1_when_n_is_1(long n)
        {
            Assert.AreEqual(1, Fibonacci.Iterative.GetFiboOf(n));
            Assert.AreEqual(1, Fibonacci.Memoized.GetFiboOf(n));
            Assert.AreEqual(1, Fibonacci.RecursiveIterative.GetFiboOf(n));
            Assert.AreEqual(1, Fibonacci.Recursive.GetFiboOf(n));
            Assert.AreEqual(1, Fibonacci.GoldenRatio.GetFiboOf(n));
            Assert.AreEqual(1, Fibonacci.DynamicProgramming.GetFibo(n));
        }

        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 3)]
        [TestCase(5, 5)]
        [TestCase(6, 8)]
        public void Should_return_fib_of_n_less_1_plus_fib_of_n_less_2(long n, long fibo)
        {
            Assert.AreEqual(fibo, Fibonacci.Iterative.GetFiboOf(n));
            Assert.AreEqual(fibo, Fibonacci.Memoized.GetFiboOf(n));
            Assert.AreEqual(fibo, Fibonacci.RecursiveIterative.GetFiboOf(n));
            Assert.AreEqual(fibo, Fibonacci.Recursive.GetFiboOf(n));
            Assert.AreEqual(fibo, Fibonacci.GoldenRatio.GetFiboOf(n));
            Assert.AreEqual(fibo, Fibonacci.DynamicProgramming.GetFibo(n));
        }

        [TestCase(40)]
        [TestCase(50)]
        [TestCase(1000)]
        public void Should_be_performant(int n)
        {
            var fiboImplementations = new System.Collections.Generic.List<Func<long, long>>
            {
                Fibonacci.Iterative.GetFiboOf,
                Fibonacci.Memoized.GetFiboOf,
                Fibonacci.RecursiveIterative.GetFiboOf,
                // Fibonacci.Recursive.GetFiboOf left out because it wont pass the test
                Fibonacci.GoldenRatio.GetFiboOf,
                Fibonacci.DynamicProgramming.GetFibo
            };
            var stopWatch = new Stopwatch();
            foreach (var fiboImplementation in fiboImplementations)
            {
                stopWatch.Start();
                fiboImplementation(n);
                stopWatch.Stop();
                Assert.LessOrEqual(stopWatch.ElapsedMilliseconds, 20);
                stopWatch.Reset();
            }
        }
    }
}