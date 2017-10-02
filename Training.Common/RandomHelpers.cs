using System;
using System.Linq;

namespace Training.Common
{
    public static class RandomHelpers
    {
        public static T[] Suffle<T>(T[] arr)
        {
            var random = new Random();
            for (var i = arr.Length - 1; 0 <= i; i--)
            {
                var j = random.Next(0, i);
                ArrayHelpers.Swap(arr, j, i);
            }
            return arr;
        }

        public static T[] SuffleSequence<T>(int start, int end) where T : IComparable
        {
            var count = end - start + 1;
            return Suffle(Enumerable.Range(start, count).Select(i => i).Cast<T>().ToArray());
        }
    }
}