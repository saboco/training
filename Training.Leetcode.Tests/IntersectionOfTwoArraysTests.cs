using Xunit;

namespace Training.Leetcode.Tests
{
    public class IntersectionOfTwoArraysTests
    {
        [Theory]
        [InlineData(new[] { 2 }, new[] { 1, 2, 2, 1 }, new[] { 2, 2 })]
        [InlineData(new[] { 9, 4 }, new[] { 4, 9, 5 }, new[] { 9, 4, 9, 8, 4 })]
        public void InterSectionTest(int[] expected, int[] arr1, int[] arr2)
        {
            var actual = IntersectionOfTwoArrays.Intersection(arr1, arr2);
            Assert.Equal(expected.Length, actual.Length);
            for (var i = 0; i < actual.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
    }
}
