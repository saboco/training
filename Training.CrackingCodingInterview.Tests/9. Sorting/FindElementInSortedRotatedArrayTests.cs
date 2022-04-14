using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class FindElementInSortedRotatedArrayTests
    {
        [Theory]
        [InlineData(5, 3, new[] { 1, 3, 4, 5, 7, 10, 14, 15, 16, 19, 20, 25 })]
        [InlineData(5, 2, new[] { 3, 4, 5, 7, 10, 14, 15, 16, 19, 20, 25, 1 })]
        [InlineData(5, 1, new[] { 4, 5, 7, 10, 14, 15, 16, 19, 20, 25, 1, 3 })]
        [InlineData(5, 0, new[] { 5, 7, 10, 14, 15, 16, 19, 20, 25, 1, 3, 4 })]
        [InlineData(5, 11, new[] { 7, 10, 14, 15, 16, 19, 20, 25, 1, 3, 4, 5 })]
        [InlineData(5, 10, new[] { 10, 14, 15, 16, 19, 20, 25, 1, 3, 4, 5, 7 })]
        [InlineData(5, 9, new[] { 14, 15, 16, 19, 20, 25, 1, 3, 4, 5, 7, 10 })]
        [InlineData(5, 8, new[] { 15, 16, 19, 20, 25, 1, 3, 4, 5, 7, 10, 14 })]
        [InlineData(5, 7, new[] { 16, 19, 20, 25, 1, 3, 4, 5, 7, 10, 14, 15 })]
        [InlineData(5, 6, new[] { 19, 20, 25, 1, 3, 4, 5, 7, 10, 14, 15, 16 })]
        [InlineData(5, 5, new[] { 20, 25, 1, 3, 4, 5, 7, 10, 14, 15, 16, 19 })]
        [InlineData(5, 4, new[] { 25, 1, 3, 4, 5, 7, 10, 14, 15, 16, 19, 20, })]
        public void FindElementTest(int elem, int expected, int[] arr)
        {
            var actual = FindElementInSortedRotatedArray.Find(arr, elem);
            Assert.Equal(expected, actual);
        }
    }
}
