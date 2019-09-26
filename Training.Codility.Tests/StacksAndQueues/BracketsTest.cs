using Xunit;
using Training.Codility.StacksAndQueues.Brackets;

namespace Training.Codility.Tests.StacksAndQueues
{
    public class BracketsTest
    {
        [Theory]
        [InlineData("{[()()]}", 1)]
        [InlineData("([)()]", 0)]
        [InlineData("{()[]()[]([[]])}", 1)]
        [InlineData("", 1)]
        [InlineData("{}[][]{()[]()[]([[]])}", 1)]
        [InlineData("{}[][]{()[)[]([[]])}", 0)]
        [InlineData(")(", 0)]
        [InlineData(")", 0)]
        [InlineData("(", 0)]
        [InlineData("(()(())())", 1)]
        [InlineData("())", 0)]
        public void Should_return_if_brackets_are_balanced(string s, int expected)
        {
            Assert.Equal(expected, Solution.Solve(s));
        }
    }
}