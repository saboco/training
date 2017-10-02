using System;
using System.Collections.Generic;

namespace Training.DataStructures
{
    public class MinHeap
    {
        private int[] _heap;
        private int _capacity = 10;

        public int Count { get; private set; }

        public MinHeap()
        {
            _heap = new int[_capacity];
        }

        public void Add(int data)
        {
            EnsureCapacity();
            _heap[Count] = data;
            Count++;
            HeapifyUp();
        }

        public int Peek()
        {
            CheckEmptiness();
            return _heap[0];
        }

        public bool IsEmpty => Count == 0;

        public int Poll()
        {
            CheckEmptiness();
            var data = Peek();
            _heap[0] = _heap[Count - 1];
            Count--;
            HeapifyDown();
            return data;
        }

        private void CheckEmptiness()
        {
            if (IsEmpty) throw new InvalidOperationException("Heap is empty");
        }

        private void HeapifyDown()
        {
            var index = 0;
            while (HasLeftChild(index))
            {
                var smallerIndex = HasRightChild(index) && GetRightChild(index) < GetLeftChild(index)
                    ? GetRightChildIndex(index)
                    : GetLeftChildIndex(index);

                if (_heap[index] < _heap[smallerIndex])
                {
                    break;
                }
                Swap(smallerIndex, index);
                index = smallerIndex;
            }
        }

        private void HeapifyUp()
        {
            var index = Count - 1;
            while (HasParent(index) && GetParent(index) > _heap[index])
            {
                Swap(GetParentIndex(index), index);
                index = GetParentIndex(index);
            }
        }

        private void Swap(int srcIndex, int dstIndex)
        {
            var temp = _heap[dstIndex];
            _heap[dstIndex] = _heap[srcIndex];
            _heap[srcIndex] = temp;
        }

        private int GetParent(int index)
        {
            return _heap[GetParentIndex(index)];
        }

        private int GetLeftChild(int index)
        {
            return _heap[GetLeftChildIndex(index)];
        }

        private int GetRightChild(int index)
        {
            return _heap[GetRightChildIndex(index)];
        }

        private static int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }

        private static int GetLeftChildIndex(int index)
        {
            return index * 2 + 1;
        }

        private static int GetRightChildIndex(int index)
        {
            return index * 2 + 2;
        }

        private bool HasLeftChild(int index)
        {
            return GetLeftChildIndex(index) < Count;
        }

        private bool HasRightChild(int index)
        {
            return GetRightChildIndex(index) < Count;
        }

        private static bool HasParent(int index)
        {
            return index >= 0;
        }

        private static void CopyHeap(IReadOnlyList<int> src, IList<int> dst)
        {
            for (var i = 0; i < src.Count; i++)
            {
                dst[i] = src[i];
            }
        }

        private void ResizeHeap()
        {
            _capacity *= 2;
            var newHeap = new int[_capacity];
            CopyHeap(_heap, newHeap);
            _heap = newHeap;
        }

        private void EnsureCapacity()
        {
            if (Count < _capacity) return;
            ResizeHeap();
        }

        public IEnumerable<int> EnumerateHeap()
        {
            var i = 0;
            while (i < Count)
            {
                yield return _heap[i++];
            }
        }
    }
}