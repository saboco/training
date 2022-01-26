using System.Collections.Generic;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class ScreenTests
    {
        [Theory]
        [InlineData(
            3,
            0,
            0,
            1,
            new[] { 0, 0, 0 },
            new[] { 0, 0, 0 },
            new[] { 0, 0, 0 },
            new[] { 1, 1, 1 },
            new[] { 1, 1, 1 },
            new[] { 1, 1, 1 })]
        [InlineData(
            3,
            1,
            1,
            1,
            new[] { 2, 2, 2 },
            new[] { 2, 0, 2 },
            new[] { 2, 2, 2 },
            new[] { 2, 2, 2 },
            new[] { 2, 1, 2 },
            new[] { 2, 2, 2 })]
        [InlineData(
            6,
            3,
            2,
            5,
            new[] { 1, 1, 1, 1, 1, 1 },
            new[] { 1, 1, 2, 2, 2, 2 },
            new[] { 1, 2, 2, 3, 2, 3 },
            new[] { 1, 1, 2, 2, 2, 3 },
            new[] { 1, 1, 2, 1, 3, 3 },
            new[] { 1, 1, 1, 3, 3, 1 }, 
            new[] { 1, 1, 1, 1, 1, 1 },
            new[] { 1, 1, 5, 5, 5, 5 },
            new[] { 1, 5, 5, 3, 5, 3 },
            new[] { 1, 1, 5, 5, 5, 3 },
            new[] { 1, 1, 5, 1, 3, 3 },
            new[] { 1, 1, 1, 3, 3, 1 })]
        public void FillInTest(int r, int x, int y, int newColor, params int[][] g)
        {
            var screen = new List<int[]>();
            for (var i = 0; i < r; i++)
            {
                screen.Add(g[i]);
            }
            var expected = new List<int[]>();
            for (var i = r; i < r * 2; i++)
            {
                expected.Add(g[i]);
            }
            var actual = screen.ToArray();
            Screen.FillIn(actual, (x,y), newColor);
            Common.AssertEqual(expected.ToArray(), actual);
        }
    }
}
