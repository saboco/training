using System;
using System.Collections.Generic;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class BinomialCoefficientTests
    {
        List<Func<int, int, int>> _actions = new List<Func<int, int, int>>
        {
            BinomialCoefficient.C,
            BinomialCoefficient.Cmemo,
            BinomialCoefficient.Cdp
        };
        [Theory]
        [InlineData(4, 2, 6)]
        [InlineData(4, 3, 4)]
        [InlineData(5, 2, 10)]
        [InlineData(5, 3, 10)]
        [InlineData(6, 4, 15)]
        public void BinomialCoefficientTest(int n, int k, int expected)
        {
            foreach (var action in _actions)
            {
                var bc = action.Invoke(n, k);
                Assert.Equal(expected, bc);
            }
        }
    }
}
