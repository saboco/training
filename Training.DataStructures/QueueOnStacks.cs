namespace Training.DataStructures
{
    public class QueueOnStacks<T>
    {
        public bool IsEmpty => _tail.IsEmpty && _head.IsEmpty;

        private readonly Stack<T> _head = new Stack<T>();
        private readonly Stack<T> _tail = new Stack<T>();

        public void Enqueue(T data)
        {
            _tail.Push(data);
        }

        public T Dequeue()
        {
            if (_head.IsEmpty)
            {
                TailToHead();
            }
            return _head.Pop();
        }

        public T Peek()
        {
            if (_head.IsEmpty)
            {
                TailToHead();
            }
            return _head.Top();
        }

        private void TailToHead()
        {
            while (!_tail.IsEmpty)
            {
                _head.Push(_tail.Pop());
            }
        }
    }
}