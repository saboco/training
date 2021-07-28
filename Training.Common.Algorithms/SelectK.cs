using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Common.Algorithms
{
    public class SelectK
    {
        public static IEnumerable<int[]> SelectKItems(int[] arr, int k)
        {
            var selected = new List<int[]>();
            SelectKItems(arr, k, new List<int>(), selected, 0);
            return selected;
        }

        private static void SelectKItems(int[] arr, int k, List<int> current, List<int[]> selected, int j)
        {
            if (current.Count == k)
            {
                selected.Add(current.ToArray());
            }
            for (var i = j; i < arr.Length; i++)
            {
                if (current.Count + 1 > k)
                { continue; }

                current.Add(arr[i]);
                SelectKItems(arr, k, current, selected, i + 1);
                current.RemoveAt(current.Count - 1);
            }
        }
    }
}
