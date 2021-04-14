using System.Collections.Generic;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class RotateMatrixTests
    {
        [Theory]
        [InlineData(4,
            new[] { 1, 2, 3, 4 }, new[] { 5, 6, 7, 8 }, new[] { 9, 10, 11, 12 }, new[] { 13, 14, 15, 16 },
            new[] { 13, 9, 5, 1 }, new[] { 14, 10, 6, 2 }, new[] { 15, 11, 7, 3 }, new[] { 16, 12, 8, 4 })]
        [InlineData(2,
            new[] { 1, 2 }, new[] { 3,4 },
            new[] { 3,1 }, new[] { 4,2 })]
        [InlineData(1,
            new[] { 1 },
            new[] { 1 })]
        [InlineData(0, new int [0])]
        [InlineData(0, null)]
        public void RotateMatrixTest(int rowsCount, params int[][] rows)
        {
            var m = new List<int[]>();
            for (var i = 0; i < rowsCount; i++)
            {
                m.Add(rows[i]);
            }
            var actual = m.ToArray();
            RotateMatrix.Rotate(actual);
            var expected = new List<int[]>();
            
            for (var i = rowsCount; i < (rows?.Length ?? 0); i++)
            {
                expected.Add(rows[i]);
            }
            for(var i = 0; i < actual.Length; i++)
            {
                for (var j = 0; j < actual.Length; j++)
                {
                    Assert.Equal(expected[i][j], actual[i][j]);
                }
            }
        }
    }
}
