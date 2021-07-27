using System;
using Training.Common;

namespace Training.DataStructures
{
    public class BinaryTree<T> where T : IComparable
    {
        public Node Root { get; }

        public BinaryTree(T data)
        {
            Root = new Node(data);
        }

        public void Add(T data)
        {
            Root.Add(data);
        }

        public void PrintInOrder(IPrint printer)
        {
            Root.PrintInOrder(printer);
        }

        public class Node
        {
            public Node Left { get; private set; }
            public Node Right { get; private set; }
            public T Data { get; }

            public Node(T data)
            {
                Data = data;
            }

            public void Add(T data)
            {
                if (data.CompareTo(Data) <= 0)
                {
                    if (Left == null)
                    {
                        Left = new Node(data);
                    }
                    else
                    {
                        Left.Add(data);
                    }
                }
                else
                {
                    if (Right == null)
                    {
                        Right = new Node(data);
                    }
                    else
                    {
                        Right.Add(data);
                    }
                }
            }

            public void PrintInOrder(IPrint printer)
            {
                Left?.PrintInOrder(printer);
                printer.Print(Data);
                Right?.PrintInOrder(printer);
            }

            public bool Contains(T value)
            {
                var compareResult = value.CompareTo(Data);
                if (compareResult == 0)
                {
                    return true;
                }

                if (compareResult < 0)
                {
                    return Left != null && Left.Contains(value);
                }
                return Right != null && Right.Contains(value);
            }
        }

        public bool Contains(T value)
        {
            return Root.Contains(value);
        }
    }
}