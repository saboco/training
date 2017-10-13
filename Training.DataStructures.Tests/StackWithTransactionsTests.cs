using NUnit.Framework;

namespace Training.DataStructures.Tests
{
    public class StackWithTransactionsTests
    {
        [Test]
        public void Should_act_like_a_normal_stack()
        {
            var stack = new StackWithTransactions<int>();
            stack.Push(5);
            stack.Push(2); // stack: [5,2]
            Assert.AreEqual(2, stack.Top());
            stack.Pop(); // stack: [5]
            Assert.AreEqual(5, stack.Top());

            var stack2 = new StackWithTransactions<int>();
            Assert.AreEqual(0, stack2.Top()); // top of an empty stack is 0
            stack2.Pop(); // pop should do nothing
        }

        [Test]
        public void Should_top_return_zero_when_empty()
        {
            var stack = new StackWithTransactions<int>();
            Assert.AreEqual(0, stack.Top());
        }

        [Test]
        public void Should_manage_transactions_with_begin_rollback_and_commit()
        {
            var stack = new StackWithTransactions<int>();
            stack.Begin();
            stack.Push(1);
            stack.Push(2); // stack [1, 2]
            Assert.AreEqual(2, stack.Top());
            Assert.IsTrue(stack.Rollback()); // stack []

            Assert.AreEqual(0, stack.Top());

            stack.Push(1);
            stack.Begin();
            stack.Push(1);
            stack.Push(2); // stack [1, 1,2]
            Assert.AreEqual(2, stack.Top());
            stack.Pop(); // stack [1, 1]
            Assert.AreEqual(1, stack.Top());
            stack.Push(5); // stack [1,1,5]
            Assert.AreEqual(5, stack.Top());

            Assert.IsTrue(stack.Commit()); // stack [1,1, 5]
            Assert.AreEqual(5, stack.Top());
            Assert.IsFalse(stack.Rollback()); // no transaction

            Assert.AreEqual(5, stack.Top());

            stack.Pop();
            Assert.AreEqual(1, stack.Top());
            stack.Pop();
            Assert.AreEqual(1, stack.Top());
            stack.Pop();
            Assert.AreEqual(0, stack.Top());
            stack.Pop();
            Assert.AreEqual(0, stack.Top());
        }

        [Test]
        public void Should_manage_embedded_transactions()
        {
            var stack = new StackWithTransactions<int>();
            stack.Push(4);
            stack.Begin(); // start transaction 1
            stack.Push(7); // stack: [4,7]
            stack.Begin(); // start transaction 2
            stack.Push(2); // stack: [4,7,2]
            Assert.IsTrue(stack.Rollback()); // rollback transaction 2
            Assert.AreEqual(7, stack.Top()); // stack: [4,7]
            stack.Begin(); // start transaction 3
            stack.Push(10); // stack: [4,7,10]
            Assert.IsTrue(stack.Commit()); // transaction 3 is committed
            Assert.AreEqual(10, stack.Top());
            Assert.IsTrue(stack.Rollback()); // rollback transaction 1
            Assert.AreEqual(4, stack.Top()); // stack: [4]
            Assert.IsFalse(stack.Commit()); // there is no open transaction
        }

        [Test]
        public void Should_manage_embeded_transactions_2()
        {
            var stack = new StackWithTransactions<int>();
            stack.Push(4);
            stack.Push(5);
            stack.Begin(); // start transaction 1
            stack.Push(7); // stack: [4,5,7]
            stack.Begin(); // start transaction 2
            stack.Push(2); // stack: [4,5,7,2]
            Assert.IsTrue(stack.Rollback()); // rollback transaction 2
            Assert.AreEqual(7, stack.Top()); // stack: [4,5,7]
            stack.Begin(); // start transaction 3
            stack.Push(10); // stack: [4,5,7,10]
            stack.Push(7); // stack: [4,5,7,10,7]
            Assert.IsTrue(stack.Commit()); // transaction 3 is committed
            Assert.AreEqual(7, stack.Top());
            Assert.IsTrue(stack.Rollback()); // rollback transaction 1
            Assert.AreEqual(5, stack.Top()); // stack: [4, 5]
            Assert.IsFalse(stack.Commit()); // there is no open transaction
        }

        [Test]
        public void Should_manage_transactions_4()
        {
            var stack = new StackWithTransactions<int>();
            stack.Push(2);
            stack.Push(4);
            stack.Push(9);

            stack.Begin();
            stack.Push(6);
            stack.Push(5);
            Assert.IsTrue(stack.Commit());

            Assert.IsFalse(stack.Rollback());

            Assert.AreEqual(5, stack.Top());

            stack.Begin();
            stack.Push(4);
            stack.Push(3);
            stack.Begin();
            stack.Push(2);
            stack.Push(1);
            Assert.AreEqual(1, stack.Top());
            stack.Pop();
            Assert.AreEqual(2, stack.Top());
            Assert.IsTrue(stack.Rollback());
            Assert.AreEqual(3, stack.Top());
            Assert.IsTrue(stack.Commit());
            stack.Pop();
            Assert.AreEqual(4, stack.Top());
            Assert.IsFalse(stack.Commit());
        }

        [Test]
        public void Should_not_throw_when_poping_an_empty_stack()
        {
            var stack = new StackWithTransactions<int>();

            Assert.DoesNotThrow(() =>
            {
                stack.Pop();
                stack.Pop();
                stack.Pop();
                stack.Pop();
                stack.Pop();
                stack.Pop();
                stack.Pop();
            });
        }

        [Test]
        public void Should_not_throw_when_toping_an_empty_stack()
        {
            var stack = new StackWithTransactions<int>();

            Assert.DoesNotThrow(() =>
            {
                stack.Top();
                stack.Top();
                stack.Top();
                stack.Top();
                stack.Top();
                stack.Top();
                stack.Top();
            });
        }

        [Test]
        public void Should_return_false_when_commiting_empty_stack()
        {
            var stack = new StackWithTransactions<int>();
            Assert.IsFalse(stack.Commit());
            Assert.IsFalse(stack.Commit());
            Assert.IsFalse(stack.Commit());
            Assert.IsFalse(stack.Commit());
            Assert.IsFalse(stack.Commit());
        }

        [Test]
        public void Should_return_false_when_rollbacking_empty_stack()
        {
            var stack = new StackWithTransactions<int>();
            Assert.IsFalse(stack.Rollback());
            Assert.IsFalse(stack.Rollback());
            Assert.IsFalse(stack.Rollback());
            Assert.IsFalse(stack.Rollback());
            Assert.IsFalse(stack.Rollback());
        }

        [Test]
        public void Should_not_throw_when_begining_several_times_from_empty_stack()
        {
            var stack = new StackWithTransactions<int>();
            Assert.DoesNotThrow(() =>
            {
                stack.Begin();
                stack.Begin();
                stack.Begin();
                stack.Begin();
                stack.Begin();
            });
        }

        [Test]
        public void Should_return_default_value_when_toping_an_empty_stack()
        {
            var stack = new StackWithTransactions<int>();
            Assert.AreEqual(default(int), stack.Top());
        }
    }
}