using System.Linq;
using NUnit.Framework;
using PredictiveKeyboardT9;

namespace PredictiveKeyboardT9Tests
{
    [TestFixture]
    public class PredictionsTests
    {
        private readonly Predictions _predictions;

        public PredictionsTests()
        {
            var words = new[] {"any", "rage", "ragee", "sage", "raid", "raider"};
            var dictionary = new Dictionary(words);
            _predictions = new Predictions(dictionary);
        }

        [TestCase(new[] {7, 2, 4, 3}, new[] {"rage", "ragee", "sage", "raid", "raider"})]
        [TestCase(new[] {7, 2, 4}, new[] {"rage", "ragee", "sage", "raid", "raider"})]
        [TestCase(new[] {7, 2, 4, 3, 3}, new[] {"ragee", "raider"})]
        [TestCase(new[] {7, 2, 4, 3, 3, 7}, new[] {"raider"})]
        [TestCase(new[] {2, 6}, new[] {"any"})]
        [TestCase(new[] {2, 2, 2}, new string[0])]
        public void Should_return_a_list_of_complete_words_in_the_dictionay_given_some_digits(int[] digits,
            string[] expected)
        {
            var actual = _predictions.GetPredictions(digits).ToArray();
            Assert.AreEqual(expected.Length, actual.Length);
            foreach (var w in actual)
            {
                Assert.IsTrue(expected.Contains(w));
            }
        }

        [Test]
        public void Should_handle_empty_digits()
        {
            var actual = _predictions.GetPredictions(new int[0]).ToArray();
            Assert.AreEqual(0, actual.Length);
        }

        [Test]
        public void Should_handle_null_digits()
        {
            var actual = _predictions.GetPredictions(null).ToArray();
            Assert.AreEqual(0, actual.Length);
        }
    }
}