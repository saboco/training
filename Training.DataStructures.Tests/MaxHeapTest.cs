using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Training.DataStructures.Tests
{
    [TestFixture]
    public class MaxHeapTest
    {
        [Test]
        public void Should_remain_a_valid_heap_when_adding_new_items()
        {
            var heap = new MaxHeap();
            var inputData = new[] { 1, 1, 1, 10, 4, 3, 5, 10, 35, 2, 100, 1, 25, 4, 35, 9, 101 };
            foreach (var i in inputData)
            {
                heap.Add(i);
            }
            AssertValidMaxHeap(heap);
        }

        [Test]
        public void Should_remain_a_valid_heap_when_polled()
        {
            var heap = new MaxHeap();
            var inputData = new[] { 1, 1, 1, 10, 4, 3, 5, 10, 35, 2, 100, 1, 25, 4, 35, 9, 101 };
            foreach (var i in inputData)
            {
                heap.Add(i);
            }
            AssertValidMaxHeap(heap);
            while (!heap.IsEmpty)
            {
                heap.Poll();
                AssertValidMaxHeap(heap);
            }
        }

        private static void AssertValidMaxHeap(MaxHeap heap)
        {
            var heapArr = heap.EnumerateHeap()
                .ToList();

            for (var i = 0; i < heapArr.Count; i++)
            {
                if (HasLeftChild(i, heapArr.Count))
                {
                    Assert.IsTrue(heapArr[i] >= GetLeftChild(heapArr, i));
                }
                if (HasRightChild(i, heapArr.Count))
                {
                    Assert.IsTrue(heapArr[i] >= GetRightChild(heapArr, i));
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
