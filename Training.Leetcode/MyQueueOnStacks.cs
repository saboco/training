using System;
using System.Collections.Generic;

namespace Training.Leetcode
{
    public class MyQueueOnStacks
    {
        readonly Stack<int> _enqueue = new Stack<int>();
        readonly Stack<int> _dequeue = new Stack<int>();

        /** Push element x to the back of queue. */
        public void Push(int x)
        {
            _enqueue.Push(x);
        }

        /** Removes the element from in front of queue and returns that element. */
        public int Pop()
        {
            EnsureDequeue();
            return _dequeue.Pop();
        }

        /** Get the front element. */
        public int Peek()
        {
            EnsureDequeue();
            return _dequeue.Peek();
        }

        /** Returns whether the queue is empty. */
        public bool Empty()
        {
            return _enqueue.Count == 0 && _dequeue.Count == 0;
        }

        private void EnsureDequeue()
        {
            if (Empty())
            {
                throw new InvalidOperationException("Operation not supported on empty queue");
            }

            if (_dequeue.Count == 0)
            {
                while (_enqueue.Count > 0)
                {
                    _dequeue.Push(_enqueue.Pop());
                }
            }
        }
    }
}
