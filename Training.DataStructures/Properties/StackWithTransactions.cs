namespace Training.DataStructures.Properties
{
    public class StackWithTransactions<T>
    {
        private readonly Stack<Stack<T>> _transactions = new Stack<Stack<T>>();
        private readonly Stack<T> _stack = new Stack<T>();

        public void Push(T data)
        {
            if (_transactions.IsEmpty)
            {
                _stack.Push(data);
                return;
            }
            _transactions.TopOrDefault().Push(data);
        }

        public void Pop()
        {
            if (_transactions.IsEmpty)
            {
                _stack.PopOrDefault();
                return;
            }
            _transactions.TopOrDefault().PopOrDefault();
        }

        public T Top()
        {
            return _transactions.IsEmpty ? _stack.TopOrDefault() : _transactions.TopOrDefault().TopOrDefault();
        }

        public void Begin()
        {
            _transactions.Push(new Stack<T>());
        }

        public bool Rollback()
        {
            if (_transactions.IsEmpty)
            {
                return false;
            }

            _transactions.PopOrDefault();
            return true;
        }

        public bool Commit()
        {
            if (_transactions.IsEmpty)
            {
                return false;
            }

            var transaction = _transactions.TopOrDefault();
            _transactions.PopOrDefault();

            var innerTransaction = _transactions.IsEmpty
                ? _stack
                : _transactions.TopOrDefault();

            Stack<T>.Append(Stack<T>.Reverse(transaction), innerTransaction);

            return true;
        }
    }
}