using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.CrackingCodingInterview
{
    public class BubbleSort
    {
        public static void Sort(int[] arr)
        {
            if (arr.Length <= 1)
            { return; }

            for (var j = 0; j < arr.Length; j++)
            {
                for (var i = 1; i < arr.Length; i++)
                {
                    if (arr[i] < arr[i - 1])
                    {
                        Swap(arr, i, i - 1);
                    }
                }
            }
        }

        private static void Swap(int[] arr, int a, int b)
        {
            var tmp = arr[a];
            arr[a] = arr[b];
            arr[b] = tmp;
        }
    }
}
