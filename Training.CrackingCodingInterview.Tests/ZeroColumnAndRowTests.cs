using System.Collections.Generic;
using System;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class ZeroColumnAndRowTests
    {
        List<Action<int[][]>> _actions = new List<Action<int[][]>>
        {
           ZeroColumnAndRow.Zero,
           ZeroColumnAndRow.Zero2,
           ZeroColumnAndRow.Zero3
        };

        [Theory]
        [InlineData(3,
            new[] { 1, 2, 3 }, new[] { 4, 0, 6 }, new[] { 7, 8, 9 },
            new[] { 1, 0, 3 }, new[] { 0, 0, 0 }, new[] { 7, 0, 9 })]
        [InlineData(4,
            new[] { 1, 2, 3 }, new[] { 4, 0, 6 }, new[] { 7, 8, 9 }, new[] { 10, 11, 12 },
            new[] { 1, 0, 3 }, new[] { 0, 0, 0 }, new[] { 7, 0, 9 }, new[] { 10, 0, 12 })]
        [InlineData(3,
            new[] { 1, 2, 3, 4 }, new[] { 5, 0, 7, 8 }, new[] { 9, 10, 11, 12 },
            new[] { 1, 0, 3, 4 }, new[] { 0, 0, 0, 0 }, new[] { 9, 0, 11, 12 })]
        [InlineData(3,
            new[] { 1, 2, 3, 0 }, new[] { 5, 0, 7, 8 }, new[] { 9, 10, 11, 12 },
            new[] { 0, 0, 0, 0 }, new[] { 0, 0, 0, 0 }, new[] { 9, 0, 11, 0 })]
        public void ZeroColumnAndRowTest(int matrixRowsCount, params int[][] allRows)
        {
            foreach (var action in _actions)
            {
                var m = new List<int[]>();
                for (var i = 0; i < matrixRowsCount; i++)
                {
                    var row= new int[allRows[i].Length];
                    Array.Copy(allRows[i], row, row.Length);
                    m.Add(row);
                }
                var actual = m.ToArray();
                action.Invoke(actual);
                var expected = new List<int[]>();
                for (var i = matrixRowsCount; i < allRows.Length; i++)
                {
                    expected.Add(allRows[i]);
                }
                for (var i = 0; i < actual.Length; i++)
                {
                    for (var j = 0; j < actual[i].Length; j++)
                    {
                        Assert.Equal(expected[i][j], actual[i][j]);
                    }
                }
            }
        }
    }
}
