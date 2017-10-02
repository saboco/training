using Training.DataStructures;

namespace Training.HackerRank.DataStructures
{
    public class BalancedBrackets
    {
        public bool IsBalanced(string str)
        {
            var stack = new Stack<char>();
            foreach (var c in str)
            {
                switch (c)
                {
                    case '{':
                        stack.Push('}');
                        break;
                    case '(':
                        stack.Push(')');
                        break;
                    case '[':
                        stack.Push(']');
                        break;
                    default:
                        if (stack.IsEmpty || c != stack.Pop()) return false;
                        break;
                }
            }
            return stack.IsEmpty;
        }
    }
}