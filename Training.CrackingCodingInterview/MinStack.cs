using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.CrackingCodingInterview
{
    public class MinStack
    {
        private Node _head;
        public class Node
        {
            public int Min { get; }
            public int Data { get; }
            public Node Previous { get; internal set; }

            public Node(int data, int min)
            {
                Data = data;
                Min = min;
            }
        }
        public void Push(int d)
        {
            var min = _head == null ? d : Math.Min(_head.Min, d);
            var n = new Node(d, min);
            n.Previous = _head;
            _head = n;
        }

        public int Pop()
        {
            if (_head == null)
            {
                throw new InvalidOperationException("Stack is empty. Cannont pop an empty stack");
            }
            var d = _head.Data;
            _head = _head.Previous;
            return d;
        }

        public int Min()
        {
            return _head == null
                ? throw new InvalidOperationException("Stack is empty. Min is not defined")
                : _head.Min;
        }

        public int Peek()
        {
            return _head == null
                 ? throw new InvalidOperationException("Stack is empty. Min is not defined")
                 : _head.Data;
        }
    }
}
