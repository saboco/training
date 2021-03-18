using System;
using Xunit;

namespace Training.Leetcode.Tests
{
    public class MyQueueOnStacksTests
    {
        [Fact]
        public void MyQueueOnStacksTest()
        {
            var queue = new MyQueueOnStacks();
            queue.Push(1);
            queue.Push(5);
            queue.Push(3);
            Assert.Equal(1, queue.Peek());
            Assert.Equal(1, queue.Pop());
            Assert.Equal(5, queue.Peek());
            Assert.False(queue.Empty());
            Assert.Equal(5, queue.Pop());
            Assert.Equal(3, queue.Pop());
            Assert.True(queue.Empty());
            Assert.Throws<InvalidOperationException>(() => queue.Peek());

            queue.Push(6);
            Assert.False(queue.Empty());
            Assert.Equal(6, queue.Peek());
            Assert.False(queue.Empty());
            Assert.Equal(6, queue.Pop());
            Assert.True(queue.Empty());
        }
    }
}
