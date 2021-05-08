using System;
using System.Collections.Generic;

namespace Training.CrackingCodingInterview
{
    public class HanoiTower
    {
        private Stack[] _rods = new Stack[3];
        private readonly int _disks;
        private int _moves = 0;
        private int _lastMovedDisk = 0;

        public HanoiTower(int disks)
        {
            _disks = disks;
            for (var i = 0; i < _rods.Length; i++)
            {
                _rods[i] = new Stack();
            }
            for (var i = disks; i > 0; i--)
            {
                _rods[0].Push(i);
            }
        }
//************************** Recursive Solution *******************************
        public Stack SolveRec()
        { 
            SolveRec(_disks, _rods[0], _rods[1], _rods[2]);
            return _rods[2];
        }
        private void SolveRec(int count, Stack source, Stack spare, Stack target)
        {   
            if(count==1){  Move(source, target); return;}
            SolveRec(count - 1, source, target, spare);
            Move(source, target);
            SolveRec(count - 1, spare, source, target);
        }

        private void Move(Stack source, Stack destination)
        { 
            if(!destination.Empty && source.Peek() > destination.Peek())
            { 
                throw new InvalidOperationException($"Invalid move. Trying to put {source.Peek()} on {destination.Peek()}");
            }
            destination.Push(source.Pop());
        }
//********************************************************************************

//**************************** Iterative Solution ********************************
        public void Solve()
        {
            var src = 0;
            var step = 0;
            var dst = IsEven(_disks) ? 1 : 2;
            while (_rods[_rods.Length - 1].Count < _disks && BestSolution())
            {
                if (CanMove(src, dst))
                {
                    Move(src, dst);
                }
                else
                {
                    (step, src, dst) = Step(step, src, dst);
                }
            }
            if (_rods[_rods.Length - 1].Count != _disks && !BestSolution())
            {
                throw new InvalidProgramException("The algorith is unlikely to find a solution");
            }
        }

        private bool BestSolution()
        {
            return _moves < (Math.Pow(2, _disks) - 1);
        }

        private static bool IsOdd(int i) => i % 2 != 0;
        private static bool IsEven(int i) => i % 2 == 0;

        private bool CanMove(int src, int dst)
        {
            return
                !_rods[src].Empty
                && _rods[src].Peek() != _lastMovedDisk // cannot move the same disk twice
                    && (_rods[dst].Empty // if destination is empty you can always move
                        || (_rods[src].Peek() < _rods[dst].Peek() // disk must be put on a bigger disk
                            && (IsOdd(_rods[src].Peek()) && IsEven(_rods[dst].Peek()) // odd disk cannot be put onto odd disks
                                || IsEven(_rods[src].Peek()) && IsOdd(_rods[dst].Peek())))); // even disks cannot be put onto even disks
        }

        private void Move(int src, int dst)
        {
            var movingDisk = _rods[src].Pop();
            _lastMovedDisk = movingDisk;
            _rods[dst].Push(movingDisk);
            _moves++;
        }

        private int Inc(int i)
        {
            return (i + 1) % _rods.Length;
        }
        private (int step, int src, int dst) Step(int step, int src, int dst)
        {
            var d = dst;
            var s = src;
            while (!CanMove(s, d))
            {
                switch (step)
                {
                    case 0:
                        d = Inc(d);
                        if (d == dst)
                        {
                            step = 1;
                        }
                        break;
                    case 1:
                        s = Inc(s);
                        d = Inc(s);
                        step = 0;
                        break;
                }
            }
            if (_rods[d].Empty)
            {
                for (var i = 0; i < 4; i++)
                {
                    d = Inc(d);
                    if (CanMove(s, d))
                    {
                        break;
                    }
                }
            }

            return (step, s, d);
        }
    }
    //********************************************************************************
}
