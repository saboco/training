using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class SelectKTests
    {
        [Theory]
        [InlineData(
            new[] { 1, 2, 3, 4, 5 },
            3,
            new[] { 1, 2, 3 },
            new[] { 1, 2, 4 },
            new[] { 1, 2, 5 },
            new[] { 1, 3, 4 },
            new[] { 1, 3, 5 },
            new[] { 1, 4, 5 },
            new[] { 2, 3, 4 },
            new[] { 2, 3, 5 },
            new[] { 2, 4, 5 },
            new[] { 3, 4, 5 })]
        public void SelectKTest(int[] arr, int k, params int[][] expected)
        {
            var selected = SelectK.SelectKItems(arr, k).ToArray();
            Common.AssertEqual(expected, selected);
        }
    }
}
