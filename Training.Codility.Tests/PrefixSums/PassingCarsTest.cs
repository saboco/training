using NUnit.Framework;
using Training.Codility.PrefixSums.PassingCars;

namespace Training.Codility.Tests.PrefixSums
{
    public class PassingCarsTest
    {
        [TestCase(new[] { 0, 1, 0, 1, 1 }, ExpectedResult = 5)]
        [TestCase(new[] { 1, 0, 1, 1 }, ExpectedResult = 2)]
        [TestCase(new[] { 0, 0, 1, 0, 1, 1 }, ExpectedResult = 8)]
        public int Should_count_the_number_of_passing_cars_on_the_road(int[] arr)
        {
            return Solution.Solve(arr);
        }
    }
}