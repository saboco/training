using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.CrackingCodingInterview
{
    public class FindElementInSortedMatrix
    {
        public static (int, int) FindElement(int[][] m, int target)
        {
            var rowIndex = -1;
            var columnIndex = -1;
            for (var r = 0; r < m.Length; r++)
            {
                var maxColIndex = m[r].Length - 1;
                if (maxColIndex >= 0 && m[r][0] <= target && target <= m[r][maxColIndex])
                {
                    columnIndex = BinarySearch(m[r], target, 0, maxColIndex);
                    if (columnIndex >= 0)
                    {
                        rowIndex = r;
                        break;
                    }
                }
            }
            return (rowIndex, columnIndex);
        }

        public static (int, int) FindElement2(int[][] m, int target)
        {
            var rowIndex = -1;
            var columnIndex = -1;

            var row = 0;
            var col = m[0].Length - 1;
            while (row < m.Length && col >= 0)
            {
                if (m[row][col] == target)
                {
                    return (row, col);
                }
                else if (m[row][col] > target)
                {
                    col--;
                }
                else
                {
                    row++;
                }
            }

            return (rowIndex, columnIndex);
        }

        private static int BinarySearch(int[] arr, int n, int lo, int hi)
        {
            if (lo > hi)
            { return -1; }

            var mid = (lo + hi) / 2;
            if (arr[mid] == n)
            { return mid; }

            else if (n < arr[mid])
            {
                return BinarySearch(arr, n, lo, mid - 1);
            }
            else
            {
                return BinarySearch(arr, n, mid + 1, hi);
            }
        }
    }
}
