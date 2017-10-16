using System;
using Training.Common;
using Training.DataStructures;

namespace Training.HackerRank.DataStructures
{
    public class ATaleOfTwoStacks
    {
        private readonly IPrint _printer;
        private readonly QueueOnStacks<int> _queue = new QueueOnStacks<int>();

        public ATaleOfTwoStacks(IPrint printer)
        {
            _printer = printer;
        }

        private enum TransactionType
        {
            Enqueue = 1,
            Dequeue = 2,
            Print = 3
        }

        private class Transaction
        {
            public TransactionType Type { get; }
            public int Data { get; }

            private Transaction(TransactionType type, int data)
            {
                Type = type;
                Data = data;
            }

            public static Transaction Parse(string rawTransaction)
            {
                var parts = rawTransaction.Split(' ');
                var transaction = (TransactionType) Enum.Parse(typeof(TransactionType), parts[0]);
                var data = TransactionType.Enqueue == transaction ? int.Parse(parts[1]) : -1;

                return new Transaction(transaction, data);
            }
        }


        public void ParseTrasaction(string line)
        {
            var transaction = Transaction.Parse(line);
            TreatTransaction(transaction);
        }

        private void TreatTransaction(Transaction transaction)
        {
            switch (transaction.Type)
            {
                case TransactionType.Enqueue:
                    _queue.Enqueue(transaction.Data);
                    break;
                case TransactionType.Dequeue:
                    _queue.Dequeue();
                    break;
                case TransactionType.Print:
                    _printer.Print(_queue.Peek());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}