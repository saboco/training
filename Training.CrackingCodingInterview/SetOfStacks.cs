using System;
using System.Collections.Generic;

namespace Training.CrackingCodingInterview
{
    public class SetOfStacks
    {
        private const int _max = 3;
        private int _current = 0;
        private List<Stack> _stacks = new List<Stack>
        {
            new Stack()
        };

        public int StacksCount => _current + 1;

        public void Push(int data)
        {
            if (_stacks[_current].Count >= _max)
            {
                _stacks.Add(new Stack());
                _current++;
            }
            _stacks[_current].Push(data);
        }

        public int Pop()
        {
            if (_stacks[_current].Empty && _current > 0)
            {
                _stacks.RemoveAt(_current);
                _current--;
            }
            return _stacks[_current].Pop();
        }

        //follow up -> PopAt
        public int PopAt(int i)
        {
            if (i < 0 || i >= _stacks.Count)
            {
                throw new ArgumentOutOfRangeException($"The specified stack index is out of rage [0-{_stacks.Count})");
            }

            return _stacks[i].Pop();
        }

        // when using PopAt the Pop method should account for eventually previous empty stacks
        public int Pop2()
        {
            while (_stacks[_current].Empty && _current > 0)
            { _current--; }
            return _stacks[_current].Pop();
        }
    }
}
