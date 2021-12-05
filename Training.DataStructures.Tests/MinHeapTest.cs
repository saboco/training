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

        [Fact]
        public void Should_remain_a_valid_heap_when_adding_and_polling_miltiple_times()
        { 
            var heap = new MinHeap();
            heap.Add(10);
            heap.Add(1);
            heap.Add(4);
            AssertValidMinHeap(heap);
            var one = heap.Poll();
            AssertValidMinHeap(heap);
            Assert.Equal(1, one);

            heap.Add(3);
            heap.Add(2);
            heap.Add(8);
            AssertValidMinHeap(heap);
            var two = heap.Poll();
            AssertValidMinHeap(heap);
            Assert.Equal(2,two);

            heap.Add(2);
            heap.Add(7);
            AssertValidMinHeap(heap);
            var two2 = heap.Poll();
            Assert.Equal(2, two2);
            AssertValidMinHeap(heap);

            heap.Add(1);
            heap.Add(6);
            heap.Add(9);
            AssertValidMinHeap(heap);
            var one2 = heap.Poll();
            Assert.Equal(1, one2);
            AssertValidMinHeap(heap);

            heap.Add(0);
            heap.Add(8);
            AssertValidMinHeap(heap);
            var zero = heap.Poll();
            Assert.Equal(0, zero);
            AssertValidMinHeap(heap);

            var three = heap.Poll();
            Assert.Equal(3, three);
            AssertValidMinHeap(heap);

            var four = heap.Poll();
            Assert.Equal(4, four);
            AssertValidMinHeap(heap);

            var six = heap.Poll();
            Assert.Equal(6,six);
            AssertValidMinHeap(heap);

            heap.Add(12);
            AssertValidMinHeap(heap);
            var seven = heap.Poll();
            Assert.Equal(7, seven);
            AssertValidMinHeap(heap);
            
            var eight = heap.Poll();
            Assert.Equal(8,eight);
            AssertValidMinHeap(heap);
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
