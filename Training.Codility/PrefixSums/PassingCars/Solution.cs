namespace Training.Codility.PrefixSums.PassingCars
{
    public class Solution
    {
        public static int Solve(int[] cars)
        {
            var carsWereGoingEast = 0;
            var carsCrossedCount = 0;

            foreach (var car in cars)
            {
                if (IsGoingEst(car))
                    carsWereGoingEast++;
                if (IsGoingWest(car))
                    carsCrossedCount += carsWereGoingEast;
                if (carsCrossedCount > 1000000000) return -1;
            }
            return carsCrossedCount;
        }

        private static bool IsGoingEst(int car)
        {
            return car == 0;
        }

        private static bool IsGoingWest(int car)
        {
            return car == 1;
        }
    }
}