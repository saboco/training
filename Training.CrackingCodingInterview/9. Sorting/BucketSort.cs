using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.CrackingCodingInterview
{
    public class BucketSort
    {
        public static void Sort(int[] arr)
        {
            var n = arr.Length;
            var buckets = new List<int>[n+1];
            var m = int.MinValue;
            for (var i = 0; i < arr.Length; i++)
            {
                m = Math.Max(arr[i], m);
            }
            for (var i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new List<int>();
            }
            for (var i = 0; i < arr.Length; i++)
            {
                var bucket = n * arr[i] / m;
                buckets[bucket].Add(arr[i]);
            }
            for (var i = 0; i < buckets.Length; i++)
            {
                buckets[i].Sort();
            }

            var index = 0;
            for (var i = 0; i < buckets.Length; i++)
            {
                for (var j = 0; j < buckets[i].Count; j++)
                {
                    arr[index] = buckets[i][j];
                    index++;
                }
            }
        }
    }
}
