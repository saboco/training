using System;

namespace Training.HackerRank.Algorithms
{
    public static class ThePowerSum
    {
        public static long GetNumberOfRepresentationsFor(int x, int n)
        {
            return GetNumberOfRepresentationsFor(x, n, 1);
        }

        public static long GetNumberOfRepresentationsFor(int x, int n, int i)
        {
            var currentPower = (int) Math.Pow(i, n);
            if (currentPower < x)
            {
                return GetNumberOfRepresentationsFor(x, n, i + 1)
                       + GetNumberOfRepresentationsFor(x - currentPower, n, i + 1);
            }
            return currentPower == x ? 1 : 0;
        }
    }
}