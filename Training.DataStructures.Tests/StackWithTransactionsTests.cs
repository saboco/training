using Training.Tests.Common;
using Xunit;

namespace Training.DataStructures.Tests
{
    public class StackWithTransactionsTests
    {
        [Fact]
        public void Should_act_like_a_normal_stack()
        {
            var stack = new StackWithTransactions<int>();
            stack.Push(5);
            stack.Push(2); // stack: [5,2]
            Assert.Equal(2, stack.Top());
            stack.Pop(); // stack: [5]
            Assert.Equal(5, stack.Top());

            var stack2 = new StackWithTransactions<int>();
            Assert.Equal(0, stack2.Top()); // top of an empty stack is 0
            stack2.Pop(); // pop should do nothing
        }

        [Fact]
        public void Should_top_return_zero_when_empty()
        {
            var stack = new StackWithTransactions<int>();
            Assert.Equal(0, stack.Top());
        }

        [Fact]
        public void Should_manage_transactions_with_begin_rollback_and_commit()
        {
            var stack = new StackWithTransactions<int>();
            stack.Begin();
            stack.Push(1);
            stack.Push(2); // stack [1, 2]
            Assert.Equal(2, stack.Top());
            Assert.True(stack.Rollback()); // stack []

            Assert.Equal(0, stack.Top());

            stack.Push(1);
            stack.Begin();
            stack.Push(1);
            stack.Push(2); // stack [1, 1,2]
            Assert.Equal(2, stack.Top());
            stack.Pop(); // stack [1, 1]
            Assert.Equal(1, stack.Top());
            stack.Push(5); // stack [1,1,5]
            Assert.Equal(5, stack.Top());

            Assert.True(stack.Commit()); // stack [1,1, 5]
            Assert.Equal(5, stack.Top());
            Assert.False(stack.Rollback()); // no transaction

            Assert.Equal(5, stack.Top());

            stack.Pop();
            Assert.Equal(1, stack.Top());
            stack.Pop();
            Assert.Equal(1, stack.Top());
            stack.Pop();
            Assert.Equal(0, stack.Top());
            stack.Pop();
            Assert.Equal(0, stack.Top());
        }

        [Fact]
        public void Should_manage_embedded_transactions()
        {
            var stack = new StackWithTransactions<int>();
            stack.Push(4);
            stack.Begin(); // start transaction 1
            stack.Push(7); // stack: [4,7]
            stack.Begin(); // start transaction 2
            stack.Push(2); // stack: [4,7,2]
            Assert.True(stack.Rollback()); // rollback transaction 2
            Assert.Equal(7, stack.Top()); // stack: [4,7]
            stack.Begin(); // start transaction 3
            stack.Push(10); // stack: [4,7,10]
            Assert.True(stack.Commit()); // transaction 3 is committed
            Assert.Equal(10, stack.Top());
            Assert.True(stack.Rollback()); // rollback transaction 1
            Assert.Equal(4, stack.Top()); // stack: [4]
            Assert.False(stack.Commit()); // there is no open transaction
        }

        [Fact]
        public void Should_manage_embeded_transactions_2()
        {
            var stack = new StackWithTransactions<int>();
            stack.Push(4);
            stack.Push(5);
            stack.Begin(); // start transaction 1
            stack.Push(7); // stack: [4,5,7]
            stack.Begin(); // start transaction 2
            stack.Push(2); // stack: [4,5,7,2]
            Assert.True(stack.Rollback()); // rollback transaction 2
            Assert.Equal(7, stack.Top()); // stack: [4,5,7]
            stack.Begin(); // start transaction 3
            stack.Push(10); // stack: [4,5,7,10]
            stack.Push(7); // stack: [4,5,7,10,7]
            Assert.True(stack.Commit()); // transaction 3 is committed
            Assert.Equal(7, stack.Top());
            Assert.True(stack.Rollback()); // rollback transaction 1
            Assert.Equal(5, stack.Top()); // stack: [4, 5]
            Assert.False(stack.Commit()); // there is no open transaction
        }

        [Fact]
        public void Should_manage_transactions_4()
        {
            var stack = new StackWithTransactions<int>();
            stack.Push(2);
            stack.Push(4);
            stack.Push(9);

            stack.Begin();
            stack.Push(6);
            stack.Push(5);
            Assert.True(stack.Commit());

            Assert.False(stack.Rollback());

            Assert.Equal(5, stack.Top());

            stack.Begin();
            stack.Push(4);
            stack.Push(3);
            stack.Begin();
            stack.Push(2);
            stack.Push(1);
            Assert.Equal(1, stack.Top());
            stack.Pop();
            Assert.Equal(2, stack.Top());
            Assert.True(stack.Rollback());
            Assert.Equal(3, stack.Top());
            Assert.True(stack.Commit());
            stack.Pop();
            Assert.Equal(4, stack.Top());
            Assert.False(stack.Commit());
        }


        [Fact]
        public void Should_not_throw_when_poping_an_empty_stack()
        {
            var stack = new StackWithTransactions<int>();

            AssertHelpers.DoesNotThrow(() =>
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

        [Fact]
        public void Should_not_throw_when_toping_an_empty_stack()
        {
            var stack = new StackWithTransactions<int>();

            AssertHelpers.DoesNotThrow(() =>
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

        [Fact]
        public void Should_return_false_when_commiting_empty_stack()
        {
            var stack = new StackWithTransactions<int>();
            Assert.False(stack.Commit());
            Assert.False(stack.Commit());
            Assert.False(stack.Commit());
            Assert.False(stack.Commit());
            Assert.False(stack.Commit());
        }

        [Fact]
        public void Should_return_false_when_rollbacking_empty_stack()
        {
            var stack = new StackWithTransactions<int>();
            Assert.False(stack.Rollback());
            Assert.False(stack.Rollback());
            Assert.False(stack.Rollback());
            Assert.False(stack.Rollback());
            Assert.False(stack.Rollback());
        }

        [Fact]
        public void Should_not_throw_when_begining_several_times_from_empty_stack()
        {
            var stack = new StackWithTransactions<int>();
            AssertHelpers.DoesNotThrow(() =>
            {
                stack.Begin();
                stack.Begin();
                stack.Begin();
                stack.Begin();
                stack.Begin();
            });
        }

        [Fact]
        public void Should_return_default_value_when_toping_an_empty_stack()
        {
            var stack = new StackWithTransactions<int>();
            Assert.Equal(default(int), stack.Top());
        }
    }
}