﻿using System;
using System.Collections.Generic;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class SortingAlgorithmsTests
    {
        List<Action<int[]>> _algorithms = new List<Action<int[]>>
        {
            //BubbleSort.Sort,
            //SelectionSort.Sort
            //MergeSort.Sort
            //QuickSort.Sort
            BucketSort.Sort
        };

        [Theory]
        [InlineData(new[] { 1, 4, 4, 6, 2, 8, 5, 0 })]
        [InlineData(new[] { 1, 4, 4, 6, 6, 2, 8, 5, 0 })]
        [InlineData(new[] { 1 })]
        [InlineData(new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 })]
        [InlineData(new int[0])]
        public void Test(int[] arr)
        {
            foreach (var algo in _algorithms)
            {
                var actual = new int[arr.Length];
                Array.Copy(arr, actual, arr.Length);
                algo.Invoke(actual);
                Assert.True(IsSorted(actual));
            }
        }
        private static bool IsSorted(int[] arr)
        {
            if (arr.Length <= 1)
            {
                return true;
            }

            var prev = arr[0];
            for (var i = 1; i < arr.Length; i++)
            {
                if (arr[i] < prev)
                {
                    return false;
                }
                prev = arr[i];
            }
            return true;
        }
    }
}

