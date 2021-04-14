using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class MinStackTests
    {
        [Fact]
        public void MinStackTest()
        { 
            var s = new MinStack();
            Assert.Throws<InvalidOperationException>(() => s.Pop());
            Assert.Throws<InvalidOperationException>(() => s.Min());
            Assert.Throws<InvalidOperationException>(() => s.Peek());
            s.Push(10);
            Assert.Equal(10, s.Min());
            s.Push(12);
            Assert.Equal(10, s.Min());
            s.Push(9);
            Assert.Equal(9, s.Min());
            var d = s.Pop();
            Assert.Equal(9, d);
            Assert.Equal(10, s.Min());
            Assert.Equal(12, s.Peek());
        }
    }
}
