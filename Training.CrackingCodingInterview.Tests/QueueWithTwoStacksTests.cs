using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class QueueWithTwoStacksTests
    {
        [Fact]
        public void QueueWithTwoStacksTest()
        { 
            var queue= new QueueWithTwoStacks();
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);
            Assert.Equal(4, queue.Count);
            Assert.Equal(2, queue.Peek());
            Assert.Equal(2, queue.Dequeue());
            Assert.Equal(3, queue.Dequeue());
            Assert.Equal(4, queue.Peek());
            Assert.Equal(2, queue.Count);
            queue.Enqueue(6);
            Assert.Equal(3, queue.Count);
            Assert.Equal(4, queue.Dequeue());
            Assert.Equal(5, queue.Dequeue());
            Assert.Equal(6, queue.Dequeue());
        }
    }
}
