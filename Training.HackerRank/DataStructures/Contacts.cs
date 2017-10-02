using System;
using Training.Common;
using Training.DataStructures;

namespace Training.HackerRank.DataStructures
{
    public class Contacts
    {
        private readonly IPrint _printer;
        private readonly Tries _contacts;

        private enum TransactionType
        {
            Add,
            Find
        }

        public Contacts(IPrint printer)
        {
            _printer = printer;
            _contacts = new Tries(printer);
        }

        public void TreatTransaction(string rawTransaction)
        {
            var parts = rawTransaction.Split(' ');
            var transaction = (TransactionType)Enum.Parse(typeof(TransactionType), parts[0], true);
            var word = parts[1];

            switch (transaction)
            {
                case TransactionType.Add:
                    _contacts.Add(word);
                    break;
                case TransactionType.Find:
                    _printer.Print(_contacts.Find(word));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}