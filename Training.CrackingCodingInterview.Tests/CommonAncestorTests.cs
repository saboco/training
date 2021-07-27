using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class CommonAncestorTests
    {
        [Theory]
        [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 4, 6, 5)]
        [InlineData(new[] { 2, 3, 4, 5, 6, 7, 8, 9 }, 2, 4, 3)]
        [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 }, 8, 9, 10)]
        [InlineData(new[] { 2, 3, 6, 7, 8, 10, 12, 13 }, 8, 12, 10)]
        [InlineData(new[] { 16 }, 0, -1, -1)]
        [InlineData(new[] { 1, 6 }, 1, 6, -1)]
        [InlineData(new[] { 1, 6, 8 }, 1, 8, 6)]
        [InlineData(new[] { 1, 6, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 }, 15, 22, 18)]
        public void CommonAncestorTest(int[] arr, int target1, int target2, int expected)
        {
            (var _, var n1, var n2) = CommonAncestor.FromArray(arr, target1, target2);
            var actual = CommonAncestor.GetCommonAncestor(n1, n2);

            if (expected == -1)
            { Assert.Null(actual); }
            else
            { Assert.Equal(expected, actual.Data); }
        }
    }
}