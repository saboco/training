using System;
using System.Collections.Generic;

namespace Training.CrackingCodingInterview
{
    public class SortAnagrams
    {
        public static void SimplestSort(string[] arr)
        {
            Array.Sort(arr, Comparer);
        }
        public static void Sort(string[] arr)
        {
            var tmp = new string[arr.Length];
            Divide(arr, tmp, 0, arr.Length - 1);

            for (var i = 0; i < tmp.Length; i++)
            {
                arr[i] = tmp[i];
            }
        }

        private static void Divide(string[] arr, string[] tmp, int leftStart, int rightEnd)
        {
            if (leftStart >= rightEnd)
            { return; }

            var mid = (leftStart + rightEnd) / 2;
            Divide(arr, tmp, leftStart, mid);
            Divide(arr, tmp, mid + 1, rightEnd);
            Merge(arr, tmp, leftStart, rightEnd);

        }
        private static void Merge(string[] arr, string[] tmp, int leftStart, int rightEnd)
        {
            var leftEnd = (leftStart + rightEnd) / 2;
            var rightStart = leftEnd + 1;
            var size = rightEnd - leftStart + 1;

            var left = leftStart;
            var right = rightStart;
            var index = leftStart;

            while (left <= leftEnd && right <= rightEnd)
            {
                if (Comparer.Compare(arr[left], arr[right]) <= 0)
                {
                    tmp[index] = arr[left];
                    left++;
                }
                else
                {
                    tmp[index] = arr[right];
                    right++;
                }
                index++;
            }
            Array.Copy(arr, left, tmp, index, leftEnd - left + 1);
            Array.Copy(arr, right, tmp, index, rightEnd - right + 1);
            Array.Copy(tmp, leftStart, arr, leftStart, size);

        }

        private static int Compare(string s1, string s2)
        {
            var c1 = s1.ToCharArray();
            Array.Sort(c1);
            var c2 = s2.ToCharArray();
            Array.Sort(c2);

            return string.Compare(new string(c1), new string(c2));
        }

        readonly static IComparer<string> Comparer = Comparer<string>.Create(Compare);

        public static bool AreAnagrams(string s1, string s2)
        {
            if (s1 == null || s2 == null)
            { return false; }
            if (s1.Length != s2.Length)
            { return false; }
            if (s1 == s2)
            { return true; }

            var counter = new Dictionary<char, int>();
            foreach (var c in s1) // O(n)
            {
                if (counter.ContainsKey(c))
                {
                    counter[c]++;
                }
                else
                {
                    counter.Add(c, 1);
                }
            }
            foreach (var c in s2) // O(n)
            {
                if (counter.ContainsKey(c))
                {
                    counter[c]--;
                }
                else
                {
                    return false;
                }
            }
            foreach (var kv in counter)
            {
                if (kv.Value != 0)
                { return false; }
            }
            return true;
        }
    }
}
