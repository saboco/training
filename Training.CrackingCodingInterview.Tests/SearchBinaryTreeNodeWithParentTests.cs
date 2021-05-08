using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class SearchBinaryTreeNodeWithParentTests
    {
        [Theory]
        [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 0, 2)]
        [InlineData(new[] { 2, 3, 4, 5, 6, 7, 8, 9 }, 4, 7)]
        [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 }, 2, 4)]
        [InlineData(new[] { 2, 3, 6, 7, 8, 10, 12, 13 }, 5, 12)]
        [InlineData(new[] { 16 }, 0, -1)]
        [InlineData(new[] { 1, 6 }, 0, 6)]
        [InlineData(new[] { 1, 6, 8 }, 1, 8)]
        [InlineData(new[] { 1, 6, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 }, 15, 22)]
        public void SearchBinaryTreeNodeWithParentTest(int[] arr, int n, int next)
        {
            var bst = SearchBinaryTreeNodeWithParent.FromArray(arr);
            var node = SearchBinaryTreeNodeWithParent.Search(arr[n], bst);
            var nextNode = SearchBinaryTreeNodeWithParent.Next(node);
            if (next == -1)
            {
                Assert.Null(nextNode);
            }
            else
            {
                Assert.Equal(next, nextNode.Data);
            }
        }
    }
}
