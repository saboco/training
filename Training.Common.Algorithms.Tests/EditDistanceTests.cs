using System;
using System.Collections.Generic;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class EditDistanceTests
    {
        List<Func<string, string, int>> _actions = new List<Func<string, string, int>>
        {
            EditDistance.Min,
            EditDistance.MinMemo,
            EditDistance.MinDp,
        };

        [Theory]
        [InlineData("GOAT", "GET", 2)]
        [InlineData("GET", "GOAT", 2)]
        [InlineData("", "GOAT", 4)]
        [InlineData("GOAT", "", 4)]
        [InlineData("AAAAAAAAAA", "AAA", 7)]
        [InlineData("AAA", "AAAAAAAAAA", 7)]
        [InlineData("AAA", "AAA", 0)]
        public void MinTest(string a, string b, int expected)
        {
            foreach (var action in _actions)
            {
                var min = action.Invoke(a, b);
                Assert.Equal(expected, min);
            }
        }

        List<Func<string, string, (int, int)[]>> _actionsReconstruction = new List<Func<string, string, (int, int)[]>>
        {
            EditDistance.MinReconstruction,
            EditDistance.MinMemoReconstruction,
            EditDistance.MinDpReconstruction,
        };

        [Theory]
        [InlineData("GOAT", "GET", new[] { 2, 1, 0, 0 }, new[] { 0, 3, 1, 0 })]
        [InlineData("GET", "GOAT", new[] { 3, 2, 1, 0 }, new[] { 0, 3, 2, 0 })]
        [InlineData("", "GOAT", new[] { 3, 3, 3, 3 }, new[] { 2, 2, 2, 2 })]
        [InlineData("GOAT", "", new[] { -1, -1, -1, -1 }, new[] { 1, 1, 1, 1 })]
        [InlineData("AAAAAAAAAA", "AAA", new[] { 2, 1, 0, -1, -1, -1, -1, -1, -1, -1 }, new[] { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1 })]
        [InlineData("AAA", "AAAAAAAAAA", new[] { 9, 8, 7, 6, 6, 6, 6, 6, 6, 6 }, new[] { 0, 0, 0, 2, 2, 2, 2, 2, 2, 2 })]
        [InlineData("AAA", "AAA", new[] { 2, 1, 0 }, new[] { 0, 0, 0 })]
        public void MinReconstructionTest(string a, string b, int[] expectedIndex, int[] expectedOperations)
        {
            foreach (var action in _actionsReconstruction)
            {
                var operations = action.Invoke(a, b);
                var expected = new List<(int, int)>();
                for (var i = 0; i < expectedOperations.Length; i++)
                {
                    expected.Add((expectedIndex[i], expectedOperations[i]));
                }
                Common.AssertEqual(expected.ToArray(), operations);
            }
        }
    }
}
