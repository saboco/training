using System.Collections.Generic;

namespace PredictiveKeyboardT9
{
    public class Predictions
    {
        private readonly Dictionary _dictionary;
        private readonly Keyboard _keyboard;

        public Predictions(Dictionary dictionary) : this(dictionary, new Keyboard())
        {
        }

        private Predictions(Dictionary dictionary, Keyboard keyboard)
        {
            _dictionary = dictionary;
            _keyboard = keyboard;
        }

        public IEnumerable<string> GetPredictions(int[] digits)
        {
            return digits == null
                ? new string[0]
                : GetPredictions(digits, 0, _dictionary.Root, "", new List<string>());
        }

        private IEnumerable<string> GetPredictions
            (IReadOnlyList<int> digits, int digitIndex, Dictionary.Node node, string word, List<string> words)
        {
            if (NoMoreDigits(digits, digitIndex))
            {
                return word == string.Empty
                    ? new string[0]
                    : new[] {word};
            }

            var digit = digits[digitIndex];

            foreach (var letter in _keyboard.GetLetters(digit))
            {
                if (!node.Contains(letter)) continue;

                var next = node.Children[letter];
                var tempWord = word + letter;
                if (IsLastDigit(digits, digitIndex))
                {
                    words.AddRange(next.GetCompleteWords(tempWord));
                }
                GetPredictions(digits, digitIndex + 1, next, word + letter, words);
            }
            return words;
        }

        private static bool NoMoreDigits(IReadOnlyCollection<int> digits, int digitIndex)
        {
            return digitIndex >= digits.Count;
        }

        private static bool IsLastDigit(IReadOnlyCollection<int> digits, int digitIndex)
        {
            return digitIndex == digits.Count - 1;
        }
    }
}