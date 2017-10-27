using System.Collections.Generic;
using Training.Common;

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
                : GetPredictions(digits, _dictionary.Root, "", new List<string>());
        }

        private IEnumerable<string> GetPredictions(IReadOnlyList<int> digits, Dictionary.Node node, string word,
            List<string> words)
        {
            if (digits.Count == 0)
            {
                return word == string.Empty
                    ? new string[0]
                    : new[] {word};
            }

            var digit = digits[0];

            foreach (var letter in _keyboard.GetLetters(digit))
            {
                if (!node.Contains(letter)) continue;

                var next = node.Children[letter];
                var tempWord = word + letter;
                if (digits.Count == 1)
                {
                    words.AddRange(next.GetCompleteWords(tempWord));
                }
                GetPredictions(ArrayHelpers.RemoveAt(digits, 0), next, word + letter, words);
            }
            return words;
        }
    }
}