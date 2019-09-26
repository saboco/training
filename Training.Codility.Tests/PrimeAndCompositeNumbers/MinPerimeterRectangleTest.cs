using Xunit;

namespace Training.Codility.Tests.PrimeAndCompositeNumbers
{
    public class MinPerimeterRectangleTest
    {
        [Theory]
        [InlineData(30, 22)]
        public void Should_return_the_minimal_perimeter_of_rectangle_or_area_n(int n, int expected)
        {
            Assert.Equal(expected, Codility.PrimeAndCompositeNumbers.MinPerimeterRectangle.Solution.Solve(n));
        }
    }
}