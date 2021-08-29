using System;
using System.Collections.Generic;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class LonguestCommonSubsequenceTests
    {
        List<Func<string, string, int>> _actions = new List<Func<string, string, int>>
        {
            LonguestCommonSubsequence.Lcs,
            LonguestCommonSubsequence.LcsMemo,
            LonguestCommonSubsequence.LcsDp
        };

        [Theory]
        [InlineData("ACHEFMGLP", "XYCEPQMLG", 4)]
        [InlineData("SBCH", "BAHI", 2)]
        [InlineData("SBCH", "XDGA", 0)]
        [InlineData("AAAAAAAA", "AAAAAA", 6)]
        [InlineData("AAAAAAAA", "A", 1)]
        [InlineData("A", "AAAAAAAAA", 1)]
        [InlineData("", "BAHI", 0)]
        [InlineData("SBCH", "", 0)]
        public void LcsTest(string a, string b, int expected)
        {
            foreach (var action in _actions)
            {
                var count = action.Invoke(a, b);
                Assert.Equal(expected, count);
            }
        }

        List<Func<string, string, string>> _actionsRecontruction = new List<Func<string, string, string>>
        {
            LonguestCommonSubsequence.LcsReconstruction,
            LonguestCommonSubsequence.LcsMemoReconstruction,
            LonguestCommonSubsequence.LcsDpReconstruction,
        };

        [Theory]
        [InlineData("ACHEFMGLP", "XYCEPQMLG", "CEML")]
        [InlineData("SBCH", "BAHI", "BH")]
        [InlineData("SBCH", "XDGA", "")]
        [InlineData("AAAAAAAA", "AAAAAA", "AAAAAA")]
        [InlineData("AAAAAAAA", "A", "A")]
        [InlineData("A", "AAAAAAAAA", "A")]
        [InlineData("", "BAHI", "")]
        [InlineData("SBCH", "", "")]
        public void LcsReconstructionTest(string a, string b, string expected)
        {
            foreach (var action in _actionsRecontruction)
            {
                var count = action.Invoke(a, b);
                Assert.Equal(expected, count);
            }
        }
    }
}
