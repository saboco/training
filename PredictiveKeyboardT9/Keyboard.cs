using System.Collections.Generic;

namespace PredictiveKeyboardT9
{
    public class Keyboard
    {
        private readonly Dictionary<int, char[]> _digitLettersMap = new Dictionary<int, char[]>
        {
            {1, new char[0]},
            {2, new[] {'a', 'b', 'c'}},
            {3, new[] {'d', 'e', 'f'}},
            {4, new[] {'g', 'h', 'i'}},
            {5, new[] {'j', 'k', 'l'}},
            {6, new[] {'m', 'n', 'o'}},
            {7, new[] {'p', 'q', 'r', 's'}},
            {8, new[] {'t', 'u', 'v'}},
            {9, new[] {'w', 'x', 'y', 'z'}},
            {0, new char[0]},
        };

        public IEnumerable<char> GetLetters(int digit)
        {
            return _digitLettersMap[digit];
        }
    }
}