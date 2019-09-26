using Xunit;
using Training.HackerRank.Algorithms;

namespace Training.HackerRank.Tests.Algorithms
{
    public class IceCreamParlorTests
    {
        [Theory]
        [InlineData(4, new[] { 1, 4, 5, 3, 2 }, "1 4")]
        [InlineData(4, new[] { 2, 2, 4, 3 }, "1 2")]
        [InlineData(100, new[] { 5, 75, 25 }, "2 3")]
        [InlineData(200, new[] { 150, 24, 79, 50, 88, 345, 3 }, "1 4")]
        public void Should_return_the_indices_of_the_bought_flavors(int money, int[] iceCreamFlavors, string expected)
        {
            var iceCreamParlor = new IceCreamParlor(money, iceCreamFlavors);
            Assert.Equal(expected, iceCreamParlor.GetFlavors());
        }
    }
}