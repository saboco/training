namespace Training.CrackingCodingInterview
{
    public class QueueWithTwoStacks
    {
        private Stack _enqueue = new Stack();
        private Stack _dequeue = new Stack();

        public int Count => _enqueue.Count + _dequeue.Count;

        public void Enqueue(int data)
        {
            _enqueue.Push(data);
        }

        public int Dequeue()
        {
            if (_dequeue.Empty)
            {
                TransferEnqueueToDequeue();
            }
            return _dequeue.Pop();
        }

        public int Peek()
        { 
            if(_dequeue.Empty)
            { 
                TransferEnqueueToDequeue();
            }
            return _dequeue.Peek();
        }

        private void TransferEnqueueToDequeue()
        { 
            while(!_enqueue.Empty)
            { 
                _dequeue.Push(_enqueue.Pop());
            }
        }
    }
}
