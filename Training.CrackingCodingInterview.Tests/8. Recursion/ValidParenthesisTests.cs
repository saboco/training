using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class ValidParenthesisTests
    {

        [Theory]
        [InlineData(1, new[] { "()" })]
        [InlineData(2, new[] { "(())", "()()" })]
        [InlineData(3, new[] { "((()))", "(()())", "(())()", "()(())", "()()()" })]
        [InlineData(4, new[] {
            "(((())))",
            "((()()))",
            "((())())",
            "((()))()",
            "(()(()))",
            "(()()())",
            "(()())()",
            "(())(())",
            "(())()()",
            "()((()))",
            "()(()())",
            "()(())()",
            "()()(())",
            "()()()()" })]
        public void GetAllValidParenthesisTest(int n, string[] expected)
        {
            var actual = ValidParenthesis.GetAllValidParenthesis(n);
            Common.AssertEqual(expected, actual);
        }
    }
}
