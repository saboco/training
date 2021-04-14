using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class FindNthNodeLinkedListTests
    {
        [Theory]
        [InlineData(3, new[] { 1, 2, 3, 4, 5 }, new[] { 4, 5 })]
        [InlineData(0, new[] { 1, 2, 3, 4, 5 }, new[] { 1, 2, 3, 4, 5 })]
        [InlineData(0, new[] { 1 }, new[] { 1 })]
        [InlineData(3, new[] { 0, 1, 3, 4 }, new[] { 4 })]
        public void FindNthNodeTest(int nth, int[] data, int[] expectedData)
        {
            var head = Node.Create(data);
            var actual = FindNthNodeLinkedList.Find(nth, head);
            var expected = Node.Create(expectedData);
            Assert.True(Node.AreEqual(expected, actual));
        }
    }
}
