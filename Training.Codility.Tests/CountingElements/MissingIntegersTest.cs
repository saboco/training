using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using Training.Codility.CountingElements.MissingInteger;
using Training.Common;

namespace Training.Codility.Tests.CountingElements
{
    public class MissingIntegersTest
    {
        [TestCase(new[] { 1, 3, 6, 4, 1, 2 }, ExpectedResult = 5, TestName = "Six positives")]
        [TestCase(new[] { 1, 2, 3 }, ExpectedResult = 4, TestName = "Three positives")]
        [TestCase(new[] { 0, 2, 3 }, ExpectedResult = 1, TestName = "0, 2, 3")]
        [TestCase(new[] { -1, -3 }, ExpectedResult = 1, TestName = "Two negetives")]
        [TestCase(new[] { 3 }, ExpectedResult = 1, TestName = "Single 3")]
        [TestCase(new[] { 0 }, ExpectedResult = 1, TestName = "Single 0")]
        [TestCase(new[] { 2 }, ExpectedResult = 1, TestName = "Single 2")]
        [TestCase(new[] { 1 }, ExpectedResult = 2, TestName = "Single 1")]
        [TestCase(new[] { -1000000, 1000000 }, ExpectedResult = 1, TestName = "Extreme values")]
        [TestCase(new[] { -1000000, 1, 2, 3, 4, 5 }, ExpectedResult = 6, TestName = "Negative Extrem value with sequence 1..5")]
        public int TestCases(int[] input)
        {
            return Solution.Solve(input);
        }

        [Test]
        public void ChaothicSequence1()
        {
            var random = new Random();
            var input = Enumerable.Range(0, 10005).Select(i => random.Next(-1000000, 1000000)).ToArray();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Solution.Solve(input);
            stopWatch.Stop();
            Assert.IsTrue(stopWatch.ElapsedMilliseconds <= 160, $"Time was {stopWatch.ElapsedMilliseconds}");
        }

        [Test]
        public void ChaothicSequence2()
        {
            var random = new Random();
            var input = Enumerable.Range(0, 10005).Select(i => random.Next(-1000000, 1000000)).ToArray();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Solution.Solve(input);
            stopWatch.Stop();
            Assert.IsTrue(stopWatch.ElapsedMilliseconds <= 160, $"Time was {stopWatch.ElapsedMilliseconds}");
        }

        [Test]
        public void ChaothicSequence3()
        {
            var random = new Random();
            var input = Enumerable.Range(0, 10005).Select(i => random.Next(-1000000, 1000000)).ToArray();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Solution.Solve(input);
            stopWatch.Stop();
            Assert.IsTrue(stopWatch.ElapsedMilliseconds <= 50, $"Time was {stopWatch.ElapsedMilliseconds}");
        }

        [Test]
        public void TestCase4_HugeData2()
        {
            var input = Enumerable.Range(1, 40000).ToArray();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Solution.Solve(input);
            stopWatch.Stop();
            Assert.IsTrue(stopWatch.ElapsedMilliseconds <= 50, $"Time was {stopWatch.ElapsedMilliseconds}");
        }

        [Test]
        public void TestCasePositiveOnly()
        {
            var arr = RandomHelpers.SuffleSequence<int>(0, 100);
            var result = Solution.Solve(arr);
            Assert.AreEqual(101, result);
        }

        [Test]
        public void TestCasePositiveOnly12()
        {
            var arr = RandomHelpers.SuffleSequence<int>(102, 200);
            var result = Solution.Solve(arr);
            Assert.AreEqual(1, result);
        }
    }
}