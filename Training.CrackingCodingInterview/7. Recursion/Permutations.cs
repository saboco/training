using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.CrackingCodingInterview
{
    public class Permutations
    {
        public static IEnumerable<int[]> GetPermutations(int[] arr)
        {
            var permutations = new List<int[]>();
            var used = new HashSet<int>();
            GetPermutations(arr, new List<int>(), used, permutations);
            return permutations;
        }

        private static void GetPermutations(int[] arr, List<int> permutation, HashSet<int> used, List<int[]> permutations)
        {
            if (permutation.Count == arr.Length)
            { permutations.Add(permutation.ToArray()); }

            for (var i = 0; i < arr.Length; i++)
            {
                if (!used.Contains(arr[i]))
                {
                    permutation.Add(arr[i]);
                    used.Add(arr[i]);
                    GetPermutations(arr, permutation, used, permutations);
                    used.Remove(arr[i]);
                    permutation.RemoveAt(permutation.Count - 1);
                }
            }
        }
    }
}
