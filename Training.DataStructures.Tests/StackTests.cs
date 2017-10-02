using System;
using NUnit.Framework;

namespace Training.DataStructures.Tests
{
    public class StackTests
    {
        [Test]
        public void Should_return_last_pushed_value_whe_top()
        {
            var stack = new Stack<int>();
            
            stack.Push(1);
            stack.Push(5);
            stack.Push(4);
            stack.Push(2);
            
            Assert.AreEqual(2, stack.Top());
        }
        
        [Test]
        public void Should_push_one_element_over_the_others()
        {
            var stack = new Stack<int>();

            stack.Push(1);
            Assert.AreEqual(1, stack.Count);
            Assert.AreEqual(1, stack.Top());
            stack.Push(5);
            Assert.AreEqual(2, stack.Count);
            Assert.AreEqual(5, stack.Top());
            stack.Push(4);
            Assert.AreEqual(3, stack.Count);
            Assert.AreEqual(4, stack.Top());
            stack.Push(2);
            Assert.AreEqual(4, stack.Count);
            Assert.AreEqual(2, stack.Top());
        }

        [Test]
        public void Should_pop_last_pushed_element()
        {
            var stack = new Stack<int>();

            stack.Push(1);
            stack.Push(5);
            stack.Push(4);
            stack.Push(2);

            Assert.AreEqual(4, stack.Count);

            stack.Pop();
            Assert.AreEqual(3, stack.Count);
            Assert.AreEqual(4, stack.Top());
        }

        [Test]
        public void Should_return_last_pushed_value_when_top()
        {
            var stack = new Stack<int>();

            stack.Push(1);
            stack.Push(5);
            stack.Push(4);
            stack.Push(2);

            Assert.AreEqual(2, stack.Top());
        }
        
        [Test]
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

        [Test]
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

        [Test]
        public void Should_not_throw_if_popingordefault_an_empty_stack()
        {
            var stack = new Stack<int>();
            Assert.DoesNotThrow(() =>
            {
                stack.PopOrDefault();
                stack.PopOrDefault();
                stack.PopOrDefault();
            });
        }

        [Test]
        public void Should_not_throw_if_topingordefault_an_empty_stack()
        {
            var stack = new Stack<int>();
            Assert.DoesNotThrow(() =>
            {
                stack.TopOrDefault();
                stack.TopOrDefault();
                stack.TopOrDefault();
            });
        }

        [Test]
        public void Should_return_default_value_when_topingordefault_empty_stack()
        {
            var stack = new Stack<int>();
            Assert.AreEqual(default(int), stack.TopOrDefault());
        }
    }
}