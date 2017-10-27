using System;
using System.Collections.Generic;
using System.Linq;

namespace Training.Common
{
    public static class ArrayHelpers
    {
        public static void Swap<T>(T[] arr, long from, long to)
        {
            var tempVal = arr[from];
            arr[from] = arr[to];
            arr[to] = tempVal;
        }

        public static IEnumerable<T[]> Permute<T>(T[] c)
        {
            if (c.Length == 0) return new T[0][];
            if (c.Length == 1) return new[] {c};

            var permutations = new HashSet<T[]>();
            for (var i = 0; i < c.Length; i++)
            {
                var first = c[i];
                var rest = RemoveAt(c, i);
                var newPermutations = Permute(rest);
                foreach (var p in newPermutations)
                {
                    var permutation = Append(new[] {first}, p);
                    if (!permutations.Contains(permutation))
                        permutations.Add(permutation);
                }
            }

            return permutations.ToArray();
        }

        public static T[] Append<T>(IReadOnlyList<T> a, IReadOnlyList<T> b)
        {
            var arrResult = new T[a.Count + b.Count];

            var k = 0;
            for (var i = 0; i < a.Count; i++, k++)
            {
                arrResult[k] = a[i];
            }
            for (var i = 0; i < b.Count; i++, k++)
            {
                arrResult[k] = b[i];
            }

            return arrResult;
        }

        public static T[] RemoveAt<T>(IReadOnlyList<T> arr, int at)
        {
            var arrResult = new T[arr.Count - 1];
            for (int i = 0, j = 0; i < arr.Count; i++)
            {
                if (i != at)
                    arrResult[j++] = arr[i];
            }
            return arrResult;
        }
        
        public static IEnumerable<T> Intersection<T>(T[] arrA, T[] arrB) where T : IComparable
        {
            if (arrA == null || arrB == null) yield break;
           
            for (int aIndex = 0, bIndex = 0; aIndex < arrA.Length && bIndex < arrB.Length;)
            {
                if (arrA[aIndex].CompareTo(arrB[bIndex]) == 0)
                {
                    var item = arrA[aIndex];
                    yield return item;

                    while (aIndex < arrA.Length && bIndex < arrB.Length
                           && item.CompareTo(arrA[aIndex]) == 0
                           && item.CompareTo(arrB[bIndex]) == 0)
                    {
                        aIndex++;
                        bIndex++;
                    }
                    continue;
                }

                if (arrA[aIndex].CompareTo(arrB[bIndex]) > 0)
                {
                    bIndex++;
                }
                else
                {
                    aIndex++;
                }
            }
        }
    }
}