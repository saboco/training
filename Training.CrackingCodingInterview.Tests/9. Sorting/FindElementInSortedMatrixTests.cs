using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class FindElementInSortedMatrixTests
    {
        readonly List<Func<int[][], int, (int, int)>> _action = new()
        {
            //FindElementInSortedMatrix.FindElement,
            FindElementInSortedMatrix.FindElement2
        };

        [Theory]
        [InlineData(
            2,
            1,
            28,
            new[] { 3, 5, 7, 9 },
            new[] { 10, 12, 15, 20 },
            new[] { 25, 28, 30, 35 },
            new[] { 36, 37, 38, 40 })]
        [InlineData(
            2,
            2,
            26,
            new[] { 1, 10, 20, 30 },
            new[] { 2, 11, 25, 32 },
            new[] { 5, 12, 26, 35 },
            new[] { 8, 27, 38, 40 })]
        [InlineData(
            0,
            0,
            3,
            new[] { 3, 5, 7, 9 },
            new[] { 10, 12, 15, 20 },
            new[] { 25, 28, 30, 35 },
            new[] { 36, 37, 38, 40 })]
        [InlineData(
            3,
            3,
            40,
            new[] { 1, 10, 20, 30 },
            new[] { 2, 11, 25, 32 },
            new[] { 5, 12, 26, 35 },
            new[] { 8, 27, 38, 40 })]
        [InlineData(
            -1,
            -1,
            0,
            new[] { 1, 10, 20, 30 },
            new[] { 2, 11, 25, 32 },
            new[] { 5, 12, 26, 35 },
            new[] { 8, 27, 38, 40 })]
        [InlineData(
            0,
            0,
            1,
            new[] { 1 })]
        [InlineData(
            -1,
            -1,
            1,
            new int[0])]
        public void FindElementTest(int expectedRow, int expectedColumn, int target, params int[][] m)
        {
            foreach (var action in _action)
            {
                var (actualRow, actualColumn) = action.Invoke(m, target);
                Assert.Equal(expectedRow, actualRow);
                Assert.Equal(expectedColumn, actualColumn);
            }
        }
    }
}
