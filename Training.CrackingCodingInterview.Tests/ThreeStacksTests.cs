using System;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class ThreeStacksTests
    {
        [Fact]
        public void TestStacks()
        {
            var s1 = ThreeStacks.GetStack();
            var s2 = ThreeStacks.GetStack();
            var s3 = ThreeStacks.GetStack();
            Assert.Throws<InvalidOperationException>(ThreeStacks.GetStack);
            Assert.Throws<InvalidOperationException>(() => s1.Pop());
            Assert.Throws<InvalidOperationException>(() => s2.Pop());
            Assert.Throws<InvalidOperationException>(() => s3.Pop());

            s1.Push(2);
            s1.Push(5);
            s3.Push(8);
            s2.Push(9);
            s1.Push(7);
            s2.Push(6);
            s3.Push(3);
            Assert.Equal(7, s1.Pop());
            Assert.Equal(3, s3.Pop());
            Assert.Equal(6, s2.Pop());
            Assert.Equal(8, s3.Pop());
            Assert.Throws<InvalidOperationException>(() => s3.Pop());
            Assert.Equal(9, s2.Pop());
            Assert.Throws<InvalidOperationException>(() => s2.Pop());
            Assert.Equal(5, s1.Pop());
            Assert.Equal(2, s1.Pop());
            Assert.Throws<InvalidOperationException>(() => s1.Pop());
        }

        [Fact]
        public void TestStacks2()
        {
            var s1 = ThreeStacks2.GetStack();
            var s2 = ThreeStacks2.GetStack();
            var s3 = ThreeStacks2.GetStack();
            Assert.Throws<InvalidOperationException>(ThreeStacks2.GetStack);

            Assert.Throws<InvalidOperationException>(() => s1.Pop());
            Assert.Throws<InvalidOperationException>(() => s2.Pop());
            Assert.Throws<InvalidOperationException>(() => s3.Pop());

            s1.Push(2);
            s1.Push(5);
            s3.Push(8);
            s2.Push(9);
            s1.Push(7);
            s2.Push(6);
            s3.Push(3);
            Assert.Equal(7, s1.Pop());
            Assert.Equal(3, s3.Pop());
            Assert.Equal(6, s2.Pop());
            Assert.Equal(8, s3.Pop());
            Assert.Throws<InvalidOperationException>(() => s3.Pop());
            Assert.Equal(9, s2.Pop());
            Assert.Throws<InvalidOperationException>(() => s2.Pop());
            Assert.Equal(5, s1.Pop());
            Assert.Equal(2, s1.Pop());
            Assert.Throws<InvalidOperationException>(() => s1.Pop());

            // full up stacks
            for (var i = 0; i < 16; i++)
            {
                s1.Push(i);
                s2.Push(i);
                s3.Push(i);
            }

            Assert.Throws<InvalidOperationException>(() => s1.Push(0));
            Assert.Throws<InvalidOperationException>(() => s2.Push(0));
            Assert.Throws<InvalidOperationException>(() => s3.Push(0));
        }
    }
}
