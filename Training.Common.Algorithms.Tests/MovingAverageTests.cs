using NUnit.Framework;

namespace Training.Common.Algorithms.Tests
{
    public class MovingAverageTests
    {
        [TestCase(4, new[] {3d, 5d, 2d, 8d, 1d, 2d, 9d, 1.5d, 7.5d, 5.5d}, new[] {4.5d, 4d, 3.25d, 5d, 3.375d, 5d, 5.875d})]
        public void Should_retun_the_moving_average_for_the_given_array_in_a_k_window(int k, double[] arr, double[] expected)
        {
            var actual = MovingAverage.GetMovingAverage(arr, k);
            var i = 0;
            foreach (var d in actual)
            {
                Assert.AreEqual(expected[i], d, 0.1);
                i++;
            }
        }
    }
}