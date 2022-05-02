using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class PointsInSameLineTests
    {
        [Theory]
        [InlineData(
            2,
            0,
            0.5,
            new[] { 2, 6, 4, 5 },
            new[] { 1, 3, 4, 5 })]
        [InlineData(
            3,
            0,
            0.5,
            new[] { 2, 6, 4, 5, 8 },
            new[] { 1, 3, 4, 5, 4 })]
        public void CountMaxPointsTest(int expectedCount, double expectedC, double expectedM, int[] xs, int[] ys)
        {
            var points = new List<PointsInSameLine.Point>();
            for (var i = 0; i < xs.Length; i++)
            {
                points.Add(new PointsInSameLine.Point(xs[i], ys[i]));
            }
            var (actualLine, actualCount) = PointsInSameLine.CountMaxPoints(points.ToArray());
            Assert.Equal(expectedCount, actualCount);
            Assert.Equal(expectedM, actualLine.M, 4);
            Assert.Equal(expectedC, actualLine.C, 4);
        }
    }
}
