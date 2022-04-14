using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Training.CrackingCodingInterview
{
    public class Subsets
    {
        public static IEnumerable<int[]> GetSubsets(int[] arr)
        {
            var sets = new List<int[]>();
            GetSubsets(arr, 0, new List<int>(), sets);
            return sets;
        }

        private static void GetSubsets(int[] arr, int index, List<int> set, List<int[]> sets)
        {
            sets.Add(set.ToArray());

            for (var i = index; i < arr.Length; i++)
            {
                set.Add(arr[i]);
                GetSubsets(arr, i + 1, set, sets);
                set.RemoveAt(set.Count - 1);
            }
        }

        public static IEnumerable<int[]> GetSubSetsIterative(int[] arr)
        {
            var sets = new List<int[]>();
            sets.Add(System.Array.Empty<int>());
            foreach (var item in arr)
            {
                var len = sets.Count;
                for (var i = 0; i < len; i++)
                {
                    var set = new List<int>(sets[i]);
                    set.Add(item);
                    sets.Add(set.ToArray());
                }
            }
            return sets;
        }

        public static IEnumerable<int[]> GetSubsetsYielding(int[] arr)
        {
            return GetSubsetsYielding(arr, 0, new List<int>());
        }

        private static IEnumerable<int[]> GetSubsetsYielding(int[] arr, int index, List<int> set)
        {
            yield return set.ToArray();

            for (var i = index; i < arr.Length; i++)
            {
                set.Add(arr[i]);
                foreach (var s in GetSubsetsYielding(arr, i + 1, set))
                {
                    yield return s;
                }
                set.RemoveAt(set.Count - 1);
            }
        }

        public static IEnumerable<int[]> GetSubsetsCombinatorics(int[] arr)
        {
            var sets = new List<int[]>();
            GetSubsetsCombinatorics(arr, 0, new HashSet<int>(), sets);
            var distinct = new HashSet<string>();
            foreach (var set in sets)
            {
                var key = GetKey(set);
                if (distinct.Add(key))
                {
                    yield return set;
                }
            }
        }

        private static string GetKey(int[] set)
        {
            var sb = new StringBuilder("");
            for (var i = 0; i < set.Length; i++)
            {
                sb.Append(set[i]);
                if (i + 1 < set.Length)
                { sb.Append(','); }
            }
            return sb.ToString();
        }

        private static void GetSubsetsCombinatorics(int[] arr, int index, HashSet<int> set, List<int[]> sets)
        {
            sets.Add(set.ToArray());

            if (index >= arr.Length)
            { return; }

            set.Add(arr[index]);
            GetSubsetsCombinatorics(arr, index + 1, set, sets);
            set.Remove(arr[index]);
            GetSubsetsCombinatorics(arr, index + 1, set, sets);
        }
    }
}
