using System.Collections.Generic;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class MergeTwoSortedArraysTests
    {
        [Theory]
        [InlineData(new[] { 3, 5, 7, 9, 10, 11, 12, 0, 0, 0 }, new[] { 2, 4, 8 })]
        [InlineData(new[] { 3, 5, 7, 9, 0, 0, 0, 0, 0, 0 }, new[] { 2, 4, 8, 10, 11, 12 })]
        [InlineData(new[] { 3, 5, 7, 9, 10, 11, 12, 0, 0, 0, 0 }, new[] { 2, 4, 6, 8 })]
        [InlineData(new[] { 3, 5, 7, 9, 0, 0, 0, 0, 0, 0, 0 }, new[] { 2, 4, 6, 8, 10, 11, 12 })]
        public void MergeTwoSortedArraysTest(int[] a, int[] b)
        {
            var aLength = a.Length - b.Length;
            var expected = new List<int>();
            var i = 0;
            var j = 0;
            while (i < aLength && j < b.Length)
            {
                if (a[i] < b[j])
                {
                    expected.Add(a[i]);
                    i++;
                }
                else
                {
                    expected.Add(b[j]);
                    j++;
                }
            }
            while (i < aLength)
            {
                expected.Add(a[i]);
                i++;
            }
            while (j < b.Length)
            {
                expected.Add(b[j]);
                j++;
            }

            MergeTwoSortedArrays.Merge(a, aLength, b, b.Length);
            Common.AssertIsSorted(a);
            Common.AssertEqual(expected.ToArray(), a);
        }
    }
}
