using System.Collections.Generic;

namespace Training.Codility.StacksAndQueues.Brackets
{
    public class Solution
    {
        public static int Solve(string s)
        {
            var stack = new Stack<char>();
            foreach (var c in s)
            {
                switch (c)
                {
                    case '{':
                        stack.Push('}');
                        break;
                    case '[':
                        stack.Push(']');
                        break;
                    case '(':
                        stack.Push(')');
                        break;
                    default:
                        if (stack.Count == 0 || stack.Pop() != c)
                        {
                            return 0;
                        }

                        break;
                }
            }
            return stack.Count == 0 ? 1 : 0;
        }
    }
}