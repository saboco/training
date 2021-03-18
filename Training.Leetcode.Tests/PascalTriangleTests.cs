using System.Linq;
using Xunit;

namespace Training.Leetcode.Tests
{
    public class PascalTriangleTests
    {
        [Fact]
        public void PascalTriangleTest()
        {
            var n = 100;
            foreach (var i in Enumerable.Range(0, n))
            {
                var triangle = PascalTriangle.Generate(i);
                Assert.Equal(i, triangle.Count);
                if (i == 0)
                {
                    Assert.Empty(triangle);
                    continue;
                }
                if(i == 1)
                {
                    Assert.Equal(1, triangle[0][0]);
                    continue;
                }
                var previousRow = triangle[0];
                for (var r = 1; r < triangle.Count; r++)
                {
                    var row = triangle[r];
                    Assert.Equal(1, row[0]);
                    for (var j = 1; j < r - 1; j++)
                    {
                        Assert.Equal(previousRow[j - 1] + previousRow[j], row[j]);
                    }
                    Assert.Equal(1, row[row.Count - 1]);
                    previousRow = row;
                }
            }
        }
    }
}
