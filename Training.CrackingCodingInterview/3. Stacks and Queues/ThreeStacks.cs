using System;
using System.Collections.Generic;

namespace Training.CrackingCodingInterview
{
    public class ThreeStacks
    {
        private static List<(int, int)> _stacks = new List<(int, int)>();
        private static int _p = 0;
        private static int _stacksCount = 0;

        // TODO/ write defragment function

        public static Stack GetStack()
        {
            if (_stacksCount >= 3)
            {
                throw new InvalidOperationException("Can't give you more stacks. Max is 3");
            }

            _stacksCount++;
            return new Stack();
        }
        public class Stack
        {
            private int _ps = -1;
            public void Push(int d)
            {
                _stacks.Add((d, _ps));
                _ps = _p;
                _p++;
            }
            public int Pop()
            {
                if (_ps == -1)
                {
                    throw new InvalidOperationException("Stack is empty. Can't pop on an empty stack");
                }
                (int d, int ps) = _stacks[_ps];
                _stacks[_ps] = (ps, -1);
                _ps = ps;
                return d;
            }
        }
    }

    public class ThreeStacks2
    {
        private static int _frames = 16;
        private static int[] _stacks = new int[_frames * 3]; // 16 frames par stack
        private static int _p = 0;
        private static int _stacksCount = 0;

        // TODO: implement Resize 

        public static Stack GetStack()
        {
            if (_stacksCount >= 3)
            {
                throw new InvalidOperationException("Can't give you more stacks. Max is 3");
            }
            var stack = new Stack(_p);
            _p += _frames;
            _stacksCount++;
            return stack;
        }

        public class Stack
        {
            private readonly int _start;
            private int _ps = -1;
            public Stack(int start)
            {
                _start = start;
            }

            public void Push(int d)
            {
                if (_ps + 1 >= _frames)
                {
                    throw new InvalidOperationException($"The stack is full. You cannot add any more frames. Max frames are: {_frames}");
                }
                _ps++;
                _stacks[_ps + _start] = d;
            }

            public int Pop()
            {
                if (_ps == -1)
                {
                    throw new InvalidOperationException("Stack is empty. Can't pop on an empty stack");
                }
                int d = _stacks[_ps + _start];
                _ps--;
                return d;
            }
        }
    }
}
