using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.CrackingCodingInterview
{
    public class MergeTwoSortedArrays
    {
        public static void Merge(int[] a, int aLength, int[] b, int bLength)
        {
            var i = aLength - 1;
            var j = bLength - 1;
            var k = aLength + bLength - 1;

            while (i >= 0 && j >= 0)
            {
                if (a[i] > b[j])
                {
                    a[k] = a[i];
                    i--;
                }
                else
                {
                    a[k] = b[j];
                    j--;
                }
                k--;
            }
            while (j >= 0)
            {
                a[k] = b[j];
                j--;
                k--;
            }
        }
    }
}
