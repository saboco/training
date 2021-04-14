using Xunit;
using System;
using System.Collections.Generic;
namespace Training.CrackingCodingInterview.Tests
{
    public class ReverseCStringTests
    {
        List<Func<char[], char[]>> _actions = new List<Func<char[], char[]>>
        {
            ReverseCString.Reverse,
            ReverseCString.ReverseWhile
        };

        [Theory]
        [InlineData(new char[] { 'a', 'b', 'c', '\0' }, new char[] { 'c', 'b', 'a', '\0' })]
        [InlineData(null, null)]
        [InlineData(new char[0], new char[0])]
        [InlineData(new char[] { '\0' }, new char[] { '\0' })]
        [InlineData(new char[] { 'a','\0' }, new char[] { 'a','\0' })]
        [InlineData(new char[] { 'a','b','\0' }, new char[] { 'b','a','\0' })]
        public void ReverseTest(char[] s, char[] expected)
        {
            foreach (var action in _actions)
            {
                var actual = action.Invoke(s);
                if (actual != null)
                {
                    Assert.Equal(actual.Length, expected.Length);
                    for (var i = 0; i < s.Length; i++)
                    {
                        Assert.Equal(expected[i], actual[i]);
                    }
                }
            }
        }
    }
}
