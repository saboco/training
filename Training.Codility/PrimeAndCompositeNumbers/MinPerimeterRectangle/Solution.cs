using System;

namespace Training.Codility.PrimeAndCompositeNumbers.MinPerimeterRectangle
{
    public class Solution
    {
        public static int Solve(int area)
        {
            var i = 1L;
            var perimeter = int.MaxValue;
            while (i * i < area)
            {
                if (area % i == 0)
                    perimeter = Math.Min(perimeter, CalculatePerimeter((int) i, (int) (area / i)));
                i++;
            }
            if (i * i == area) perimeter = Math.Min(perimeter, CalculatePerimeter((int) i, (int) (area / i)));
            return perimeter;
        }

        private static int CalculatePerimeter(int a, int b)
        {
            return 2 * (a + b);
        }
    }
}
