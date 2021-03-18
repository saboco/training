using System.Collections.Generic;

namespace Training.Leetcode
{
    public class PascalTriangle
    {
        public static IList<IList<int>> Generate(int numRows)
        {
            if (numRows == 0)
            {
                return new List<IList<int>>();
            }

            if (numRows == 1)
            {
                return new List<IList<int>> { new List<int> { 1 } };
            }

            var triangle = new List<IList<int>>
            {
                new List<int>{ 1 },
                new List<int>{ 1,1 }
            };

            for (var i = 2; i < numRows; i++)
            {
                var previousRow = triangle[i - 1];
                var row = new List<int> { 1 };
                for (var j = 1; j < i; j++)
                {
                    row.Add(previousRow[j - 1] + previousRow[j]);
                }
                row.Add(1);
                triangle.Add(row);
            }

            return triangle;
        }
    }
}
