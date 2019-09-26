using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Training.DataStructures.Tests
{
    public class MinHeapTest
    {
        [Fact]
        public void Should_remain_a_valid_heap_when_adding_new_items()
        {
            var heap = new MinHeap();
            var inputData = new[] { 1, 1, 1, 10, 4, 3, 5, 10, 35, 2, 100, 1, 25, 4, 35, 9, 101 };
            foreach (var i in inputData)
            {
                heap.Add(i);
            }
            AssertValidMinHeap(heap);
        }

        [Fact]
        public void Should_remain_a_valid_heap_when_polled()
        {
            var heap = new MinHeap();
            var inputData = new[] { 1, 1, 1, 10, 4, 3, 5, 10, 35, 2, 100, 1, 25, 4, 35, 9, 101 };
            foreach (var i in inputData)
            {
                heap.Add(i);
            }
            AssertValidMinHeap(heap);
            while (!heap.IsEmpty)
            {
                heap.Poll();
                AssertValidMinHeap(heap);
            }
        }

        private static void AssertValidMinHeap(MinHeap heap)
        {
            var heapArr = heap.EnumerateHeap()
                .ToList();

            for (var i = 0; i < heapArr.Count; i++)
            {
                if (HasLeftChild(i, heapArr.Count))
                {
                    Assert.True(heapArr[i] <= GetLeftChild(heapArr, i));
                }
                if (HasRightChild(i, heapArr.Count))
                {
                    Assert.True(heapArr[i] <= GetRightChild(heapArr, i));
                }
            }
        }

        private static int GetLeftChild(IReadOnlyList<int> arr, int index)
        {
            return arr[GetLeftChildIndex(index)];
        }

        private static int GetRightChild(IReadOnlyList<int> arr, int index)
        {
            return arr[GetRightChildIndex(index)];
        }

        private static int GetLeftChildIndex(int index)
        {
            return index * 2 + 1;
        }

        private static int GetRightChildIndex(int index)
        {
            return index * 2 + 2;
        }

        private static bool HasLeftChild(int index, int size)
        {
            return GetLeftChildIndex(index) < size;
        }

        private static bool HasRightChild(int index, int size)
        {
            return GetRightChildIndex(index) < size;
        }
    }
}
