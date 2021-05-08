using System;

namespace Training.CrackingCodingInterview
{
    public class Stack
    {
        public class Node
        {
            public Node Previous { get; }
            public int Data { get; }
            public Node(Node previous, int data)
            {
                Data = data;
                Previous = previous;
            }
        }

        private Node _head;

        public bool Empty => _head == null;
        public int Count { get; set; }

        public void Push(int data)
        {
            var node = new Node(_head, data);
            _head = node;
            Count++;
        }

        public int Pop()
        {
            if (Empty)
            {
                throw new InvalidOperationException("Stack is empty. Can't Pop an empty stack");
            }
            var data = _head.Data;
            _head = _head.Previous;
            Count--;
            return data;
        }

        public int Peek()
        {
            if (Empty)
            {
                throw new InvalidOperationException("Stack is empty. Can't Peek an empty stack");
            }
            return _head.Data;
        }
    }

    public class Stack<T>
    {
        public class Node
        {
            public Node Previous { get; }
            public T Data { get; }
            public Node(Node previous, T data)
            {
                Data = data;
                Previous = previous;
            }
        }

        private Node _head;

        public bool Empty => _head == null;
        public int Count { get; set; }

        public void Push(T data)
        {
            var node = new Node(_head, data);
            _head = node;
            Count++;
        }

        public T Pop()
        {
            if (Empty)
            {
                throw new InvalidOperationException("Stack is empty. Can't Pop an empty stack");
            }
            var data = _head.Data;
            _head = _head.Previous;
            Count--;
            return data;
        }

        public T Peek()
        {
            if (Empty)
            {
                throw new InvalidOperationException("Stack is empty. Can't Peek an empty stack");
            }
            return _head.Data;
        }
    }
}
