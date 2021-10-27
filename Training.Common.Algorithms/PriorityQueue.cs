using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Common.Algorithms
{
    public class PriorityQueue<T>
    {
        private T[] _heap;
        private Func<T, T, bool> _lessThan;
        private int _capacity = 10;

        public int Count { get; private set; }

        public PriorityQueue(Func<T, T, bool> lessThan)
        {
            _heap = new T[_capacity];
            _lessThan = lessThan;
        }

        public void Enqueue(T data)
        {
            EnsureCapacity();
            _heap[Count] = data;
            Count++;
            HeapifyUp();
        }

        public T Peek()
        {
            CheckEmptiness();
            return _heap[0];
        }

        public bool IsEmpty => Count == 0;

        public T Dequeue()
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
            if (IsEmpty)
            {
                throw new InvalidOperationException("Heap is empty");
            }
        }

        private void HeapifyDown()
        {
            var index = 0;
            while (HasLeftChild(index))
            {
                var smallerIndex = HasRightChild(index) && _lessThan(GetRightChild(index), GetLeftChild(index))
                    ? GetRightChildIndex(index)
                    : GetLeftChildIndex(index);

                if (_lessThan(_heap[index], _heap[smallerIndex]))
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
            while (HasParent(index) && _lessThan(GetParent(index), _heap[index]))
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

        private T GetParent(int index)
        {
            return _heap[GetParentIndex(index)];
        }

        private T GetLeftChild(int index)
        {
            return _heap[GetLeftChildIndex(index)];
        }

        private T GetRightChild(int index)
        {
            return _heap[GetRightChildIndex(index)];
        }

        private static int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }

        private static int GetLeftChildIndex(int index)
        {
            return (index * 2) + 1;
        }

        private static int GetRightChildIndex(int index)
        {
            return (index * 2) + 2;
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

        private static void CopyHeap(IReadOnlyList<T> src, IList<T> dst)
        {
            for (var i = 0; i < src.Count; i++)
            {
                dst[i] = src[i];
            }
        }

        private void ResizeHeap()
        {
            _capacity *= 2;
            var newHeap = new T[_capacity];
            CopyHeap(_heap, newHeap);
            _heap = newHeap;
        }

        private void EnsureCapacity()
        {
            if (Count < _capacity)
            {
                return;
            }

            ResizeHeap();
        }

        public IEnumerable<T> EnumerateHeap()
        {
            var i = 0;
            while (i < Count)
            {
                yield return _heap[i++];
            }
        }
    }
}
