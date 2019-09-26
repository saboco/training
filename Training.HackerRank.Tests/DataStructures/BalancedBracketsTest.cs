using Xunit;
using Training.HackerRank.DataStructures;

namespace Training.HackerRank.Tests.DataStructures
{    
    public class BalancedBracketsTest
    {
        [Fact]
        public void Should_print_yes_when_brackets_are_balanced()
        {
            var brackets = new[] { "{}", "[]", "()", "{[()]}", "{{[[(())]]}}" };
            var sut = new BalancedBrackets();

            foreach (var bracket in brackets)
            {
                var isBalanced = sut.IsBalanced(bracket);
                Assert.True(isBalanced);
            }
            
        }

        [Fact]
        public void Should_print_false_when_brackets_are_unbalanced()
        {
            var brackets = new[] { "}}", "(", "[[", "]]", "((", "))", "{]", "{)", "(}", "(]", "[)", "[}", "{[(])}", "{[{[(())]]}}", "{{[[(())]}}" };

            var sut = new BalancedBrackets();
            foreach (var bracket in brackets)
            {
                var isBalanced = sut.IsBalanced(bracket);
                Assert.False(isBalanced);
            }
        }
    }
}
