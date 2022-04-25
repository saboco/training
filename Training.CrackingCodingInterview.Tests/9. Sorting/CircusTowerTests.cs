using System.Collections.Generic;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class CircusTowerTests
    {
        [Theory]
        [InlineData(
            6,
            new[] { 65, 70, 56, 75, 60, 68 },
            new[] { 100, 150, 90, 190, 95, 110 })]
        [InlineData(
            5,
            new[] { 65, 70, 56, 75, 60, 68 },
            new[] { 90, 150, 90, 190, 95, 110 })]
        public void MaxHeightTest(int expected, int[] heights, int[] weights)
        {
            Assert.Equal(heights.Length, weights.Length);
            var people = new List<(int, int)>();
            for (var i = 0; i < weights.Length; i++)
            {
                people.Add((heights[i], weights[i]));
            }
            var actual = CircusTower.MaxHeight(people.ToArray());
            Assert.Equal(expected, actual);
        }
    }
}
