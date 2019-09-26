using System;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Training.Codility.CountingElements.MissingInteger;
using Training.Common;

namespace Training.Codility.Tests.CountingElements
{
    public class MissingIntegersTest
    {
        [Theory]
        [InlineData(new[] { 1, 3, 6, 4, 1, 2 }, 5)] //, TestName = "Six positives"
        [InlineData(new[] { 1, 2, 3 }, 4)] // , TestName = "Three positives"
        [InlineData(new[] { 0, 2, 3 }, 1)] //, TestName = "0, 2, 3"
        [InlineData(new[] { -1, -3 }, 1)] //, TestName = "Two negetives"
        [InlineData(new[] { 3 }, 1)] // , TestName = "Single 3"
        [InlineData(new[] { 0 }, 1)] // , TestName = "Single 0"
        [InlineData(new[] { 2 }, 1)] // , TestName = "Single 2"
        [InlineData(new[] { 1 }, 2)] // , TestName = "Single 1"
        [InlineData(new[] { -1000000, 1000000 }, 1)] // , TestName = "Extreme values"
        [InlineData(new[] { -1000000, 1, 2, 3, 4, 5 }, 6)] // , TestName = "Negative Extrem value with sequence 1..5"
        public void TestCases(int[] input, int expected)
        {
            Assert.Equal(expected, Solution.Solve(input));
        }

        [Fact]
        public void ChaothicSequence1()
        {
            var random = new Random();
            var input = Enumerable.Range(0, 10005).Select(i => random.Next(-1000000, 1000000)).ToArray();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Solution.Solve(input);
            stopWatch.Stop();
            Assert.True(stopWatch.ElapsedMilliseconds <= 160, $"Time was {stopWatch.ElapsedMilliseconds}");
        }

        [Fact]
        public void ChaothicSequence2()
        {
            var random = new Random();
            var input = Enumerable.Range(0, 10005).Select(i => random.Next(-1000000, 1000000)).ToArray();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Solution.Solve(input);
            stopWatch.Stop();
            Assert.True(stopWatch.ElapsedMilliseconds <= 160, $"Time was {stopWatch.ElapsedMilliseconds}");
        }

        [Fact]
        public void ChaothicSequence3()
        {
            var random = new Random();
            var input = Enumerable.Range(0, 10005).Select(i => random.Next(-1000000, 1000000)).ToArray();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Solution.Solve(input);
            stopWatch.Stop();
            Assert.True(stopWatch.ElapsedMilliseconds <= 50, $"Time was {stopWatch.ElapsedMilliseconds}");
        }

        [Fact]
        public void TestCase4_HugeData2()
        {
            var input = Enumerable.Range(1, 40000).ToArray();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Solution.Solve(input);
            stopWatch.Stop();
            Assert.True(stopWatch.ElapsedMilliseconds <= 50, $"Time was {stopWatch.ElapsedMilliseconds}");
        }

        [Fact]
        public void TestCasePositiveOnly()
        {
            var arr = RandomHelpers.SuffleSequence<int>(0, 100);
            var result = Solution.Solve(arr);
            Assert.Equal(101, result);
        }

        [Fact]
        public void TestCasePositiveOnly12()
        {
            var arr = RandomHelpers.SuffleSequence<int>(102, 200);
            var result = Solution.Solve(arr);
            Assert.Equal(1, result);
        }
    }
}