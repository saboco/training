using NUnit.Framework;

namespace Training.Codility.Tests.PrimeAndCompositeNumbers
{
    public class MinPerimeterRectangleTest
    {
        [TestCase(30, ExpectedResult = 22)]
        public int Should_return_the_minimal_perimeter_of_rectangle_or_area_n(int n)
        {
            return Codility.PrimeAndCompositeNumbers.MinPerimeterRectangle.Solution.Solve(n);
        }
    }
}