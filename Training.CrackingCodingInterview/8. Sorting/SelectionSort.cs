using System;

namespace Training.CrackingCodingInterview
{
    public class SelectionSort
    {
        public static void Sort(int[] arr)
        {
            var min = Int32.MaxValue;
            var index = -1;
            for (var i = 0; i < arr.Length; i++)
            {
                for (var j = i; j < arr.Length; j++)
                {
                    if(arr[j] < min)
                    { 
                        min = arr[j];
                        index=j;
                    }
                }
                Swap(arr, index, i);
                min = Int32.MaxValue;
            }
        }

        private static void Swap(int[] arr, int a, int b)
        { 
            var tmp=arr[a];
            arr[a]=arr[b];
            arr[b]=tmp;
        }
    }
}
