using System;

namespace Training.DataStructures
{
    public class Stack<T>
    {
        public bool IsEmpty => _head == null;
        public int Count { get; private set; }

        private Node _head;

        public void Push(T data)
        {
            var node = new Node(data) {Next = _head};
            _head = node;
            Count++;
        }

        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Stack is empty");
            }

            return InternalPop();
        }

        public T PopOrDefault()
        {
            return IsEmpty ? default(T) : InternalPop();
        }

        public T Top()
        {
            return IsEmpty ? throw new InvalidOperationException("Stack is empty") : InternalTop();
        }

        public T TopOrDefault()
        {
            return IsEmpty ? default(T) : InternalTop();
        }

        public static Stack<T> Reverse(Stack<T> stack)
        {
            var s = new Stack<T>();
            while (!stack.IsEmpty)
            {
                s.Push(stack.Pop());
            }
            return s;
        }

        public static void Append(Stack<T> src, Stack<T> dst)
        {
            while (!src.IsEmpty)
            {
                dst.Push(src.Pop());
            }
        }

        private T InternalPop()
        {
            var data = _head.Data;
            _head = _head.Next;
            Count--;
            return data;
        }

        private T InternalTop()
        {
            return _head.Data;
        }

        public class Node
        {
            public T Data { get; }
            public Node Next { get; set; }

            public Node(T data)
            {
                Data = data;
            }
        }
    }
}