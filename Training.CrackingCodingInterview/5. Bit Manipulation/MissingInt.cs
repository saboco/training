using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.CrackingCodingInterview
{
    public class MissingInt
    {
        public static int Find(int[] arr)
        {
            if (arr == null)
            { return -1; }

            var prev = GetJBit(arr[0], 0);
            if (prev)
            { return 0; }

            for (var i = 1; i < arr.Length; i++)
            {
                var current = GetJBit(arr[i], 0);
                if (current == prev)
                {
                    return i;
                }
                prev = current;
            }

            return arr.Length;
        }

        private static bool GetJBit(int x, int index)
        {
            return (x & (1 << index)) > 0;
        }
    }
}
