using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class SortStackTests
    {
        [Fact]
        public void SortStackTestWithList()
        { 
            var s = new Stack();
            s.Push(2);
            s.Push(5);
            s.Push(10);
            s.Push(6);
            s.Push(1);
            s.Push(10);
            SortStack.SortUsingList(s);
            var prev = s.Pop();
            while(!s.Empty)
            {   
                var current = s.Pop();
                Assert.True(prev <= current);
                prev = current;
            }
        }

        [Fact]
        public void SortStackTest()
        { 
            var s = new Stack();
            s.Push(2);
            s.Push(5);
            s.Push(10);
            s.Push(6);
            s.Push(1);
            s.Push(10);
            var sorted = SortStack.Sort(s);
            var prev = sorted.Pop();
            while(!sorted.Empty)
            {   
                var current = sorted.Pop();
                Assert.True(prev <= current);
                prev = current;
            }
        }

        [Fact]
        public void SortStackTestWithoutResturning()
        { 
            var s = new Stack();
            s.Push(2);
            s.Push(5);
            s.Push(10);
            s.Push(6);
            s.Push(1);
            s.Push(10);
            SortStack.SortWithoutReturning(s);
            var prev = s.Pop();
            while(!s.Empty)
            {   
                var current = s.Pop();
                Assert.True(prev <= current);
                prev = current;
            }
        }
    }
}
