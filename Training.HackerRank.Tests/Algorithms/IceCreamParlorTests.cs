using NUnit.Framework;
using Training.HackerRank.Algorithms;

namespace Training.HackerRank.Tests.Algorithms
{
    public class IceCreamParlorTests
    {
        [TestCase(4, new[] {1, 4, 5, 3, 2}, ExpectedResult = "1 4")]
        [TestCase(4, new[] {2, 2, 4, 3}, ExpectedResult = "1 2")]
        [TestCase(100, new[] {5, 75, 25}, ExpectedResult = "2 3")]
        [TestCase(200, new[] {150, 24, 79, 50, 88, 345, 3}, ExpectedResult = "1 4")]
        public string Should_return_the_indices_of_the_bought_flavors(int money, int[] iceCreamFlavors)
        {
            var iceCreamParlor = new IceCreamParlor(money, iceCreamFlavors);
            return iceCreamParlor.GetFlavors();
        }
    }
}