using System;
using Training.Tests.Common;
using Xunit;

namespace Training.DataStructures.Tests
{
    public class StackTests
    {
        [Fact]
        public void Should_return_last_pushed_value_whe_top()
        {
            var stack = new Stack<int>();
            
            stack.Push(1);
            stack.Push(5);
            stack.Push(4);
            stack.Push(2);
            
            Assert.Equal(2, stack.Top());
        }
        
        [Fact]
        public void Should_push_one_element_over_the_others()
        {
            var stack = new Stack<int>();

            stack.Push(1);
            Assert.Equal(1, stack.Count);
            Assert.Equal(1, stack.Top());
            stack.Push(5);
            Assert.Equal(2, stack.Count);
            Assert.Equal(5, stack.Top());
            stack.Push(4);
            Assert.Equal(3, stack.Count);
            Assert.Equal(4, stack.Top());
            stack.Push(2);
            Assert.Equal(4, stack.Count);
            Assert.Equal(2, stack.Top());
        }

        [Fact]
        public void Should_pop_last_pushed_element()
        {
            var stack = new Stack<int>();

            stack.Push(1);
            stack.Push(5);
            stack.Push(4);
            stack.Push(2);

            Assert.Equal(4, stack.Count);

            stack.Pop();
            Assert.Equal(3, stack.Count);
            Assert.Equal(4, stack.Top());
        }

        [Fact]
        public void Should_return_last_pushed_value_when_top()
        {
            var stack = new Stack<int>();

            stack.Push(1);
            stack.Push(5);
            stack.Push(4);
            stack.Push(2);

            Assert.Equal(2, stack.Top());
        }
        
        [Fact]
        public void Should_throw_if_poping_an_empty_stack()
        {
            var stack = new Stack<int>();
            Assert.Throws<InvalidOperationException>(() =>
            {
                stack.Pop();
                stack.Pop();
                stack.Pop();
            });
        }

        [Fact]
        public void Should_throw_if_toping_an_empty_stack()
        {
            var stack = new Stack<int>();
            Assert.Throws<InvalidOperationException>(() =>
            {
                stack.Top();
                stack.Top();
                stack.Top();
            });
        }

        [Fact]
        public void Should_not_throw_if_popingordefault_an_empty_stack()
        {
            var stack = new Stack<int>();
            AssertHelpers.DoesNotThrow(() =>
            {
                stack.PopOrDefault();
                stack.PopOrDefault();
                stack.PopOrDefault();
            });
        }

        [Fact]
        public void Should_not_throw_if_topingordefault_an_empty_stack()
        {
            var stack = new Stack<int>();
            AssertHelpers.DoesNotThrow(() =>
            {
                stack.TopOrDefault();
                stack.TopOrDefault();
                stack.TopOrDefault();
            });
        }

        [Fact]
        public void Should_return_default_value_when_topingordefault_empty_stack()
        {
            var stack = new Stack<int>();
            Assert.Equal(default(int), stack.TopOrDefault());
        }
    }
}