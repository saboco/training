using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class SetOfStacksTests
    {
        [Fact]
        public void SetOfStacksTest()
        {
            var s = new SetOfStacks();
            Assert.Equal(1, s.StacksCount);
            s.Push(1);
            s.Push(2);
            s.Push(3);
            s.Push(4);
            Assert.Equal(2, s.StacksCount);
            Assert.Equal(4, s.Pop());
            Assert.Equal(3, s.Pop());
            Assert.Equal(2, s.Pop());
            Assert.Equal(1, s.Pop());
            Assert.Throws<InvalidOperationException>(() => s.Pop());

            s.Push(1); // stack 0
            s.Push(2);
            s.Push(3);
            s.Push(4); // stack 1
            s.Push(5);
            s.Push(6);
            s.Push(7); // stack 2

            Assert.Equal(6, s.PopAt(1));
            Assert.Equal(5, s.PopAt(1));
            Assert.Equal(4, s.PopAt(1));
            Assert.Throws<InvalidOperationException>(() => s.PopAt(1));
            Assert.Throws<ArgumentOutOfRangeException>(() => s.PopAt(3));
            Assert.Throws<ArgumentOutOfRangeException>(() => s.PopAt(-1));
            Assert.Equal(3, s.StacksCount);
            // for follow up use Pop2
            Assert.Equal(7, s.Pop2());
            Assert.Equal(3, s.Pop2());
            Assert.Equal(1, s.StacksCount);
        }
    }
}
