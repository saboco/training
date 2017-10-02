using NUnit.Framework;
using Training.Codility.StacksAndQueues.Brackets;

namespace Training.Codility.Tests.StacksAndQueues
{
    public class BracketsTest
    {
        [TestCase("{[()()]}", ExpectedResult = 1)]
        [TestCase("([)()]", ExpectedResult = 0)]
        [TestCase("{()[]()[]([[]])}", ExpectedResult = 1)]
        [TestCase("", ExpectedResult = 1)]
        [TestCase("{}[][]{()[]()[]([[]])}", ExpectedResult = 1)]
        [TestCase("{}[][]{()[)[]([[]])}", ExpectedResult = 0)]
        [TestCase(")(", ExpectedResult = 0)]
        [TestCase(")", ExpectedResult = 0)]
        [TestCase("(", ExpectedResult = 0)]
        [TestCase("(()(())())", ExpectedResult = 1)]
        [TestCase("())", ExpectedResult = 0)]
        public int Should_return_if_brackets_are_balanced(string s)
        {
            return Solution.Solve(s);
        }
    }
}