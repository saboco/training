using System.Collections.Generic;

namespace Training.CrackingCodingInterview
{
    public class ValidParenthesis
    {
        public static string[] GetAllValidParenthesis(int n)
        {
            var parenthesis = new List<string>();
            var current = new List<char> { '(' };

            GetAllValidParenthesis(n - 1, 1, current, parenthesis);

            return parenthesis.ToArray();
        }

        private static void GetAllValidParenthesis(int n, int close, List<char> current, List<string> parenthesis)
        {
            if (n == 0 && close == 0)
            {
                parenthesis.Add(new string(current.ToArray()));
            }

            if (n > 0)
            {
                current.Add('(');
                GetAllValidParenthesis(n - 1, close + 1, current, parenthesis);
                current.RemoveAt(current.Count - 1);
            }

            if (close > 0)
            {
                current.Add(')');
                GetAllValidParenthesis(n, close - 1, current, parenthesis);
                current.RemoveAt(current.Count - 1);
            }
        }
    }
}
