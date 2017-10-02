using Training.Common;
using Training.DataStructures;

namespace Training.HackerRank.DataStructures
{
    public class FindTheRunningMedian
    {
        private readonly MaxHeap _leftHeap = new MaxHeap();
        private readonly MinHeap _rightHeap = new MinHeap();

        private double _median;
        private readonly IPrint _printer;

        public FindTheRunningMedian(IPrint printer)
        {
            _printer = printer;
        }

        public void Add(int i)
        {
            if (_leftHeap.IsEmpty && _rightHeap.IsEmpty)
                _leftHeap.Add(i);

            else if (_rightHeap.Count < _leftHeap.Count)
            {
                if (_leftHeap.Peek() < i)
                    _rightHeap.Add(i);
                else
                {
                    var temp = _leftHeap.Poll();
                    _leftHeap.Add(i);
                    _rightHeap.Add(temp);
                }
            }
            else
            {
                if (i < _rightHeap.Peek())
                    _leftHeap.Add(i);
                else
                {
                    var temp = _rightHeap.Poll();
                    _rightHeap.Add(i);
                    _leftHeap.Add(temp);
                }
            }

            UpdateMedian();
            _printer.Print(_median);
        }

        private void UpdateMedian()
        {
            if (_leftHeap.Count == _rightHeap.Count)
                _median = CalculateMedian(_leftHeap.Peek(), _rightHeap.Peek());

            else if (_leftHeap.Count < _rightHeap.Count) _median = _rightHeap.Peek();
            else if (_rightHeap.Count < _leftHeap.Count) _median = _leftHeap.Peek();
        }

        private static double CalculateMedian(double a, double b)
        {
            return (a + b) / 2d;
        }
    }
}