using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.CrackingCodingInterview
{
    public class NextSmallestAndLargestNumber
    {
        public static (int, int) GetNextSmallestAndNextLargestWithSame1BitsThat(int n)
        {
            if (n == 0)
            { return (0, 0); }
            if (n + 1 == 0)
            { return (n, n); }
            if (n - 1 == 0)
            { return (n, n+1); }

            return (NextSmallest(n), NextLargest(n));
        }

        private static bool IsBitOff(int i, int n)
        { return (n & (1 << i)) == 0; }

        private static bool IsBitOn(int i, int n)
        { return !IsBitOff(i, n); }

        private static int TurnBitOn(int i, int n)
        {
            return n | (1 << i);
        }

        private static int TurnBitOff(int i, int n)
        {
            var mask = ~(1 << i);
            return n & mask;
        }

        private static int NextLargest(int n)
        {
            var i = 0;
            while (IsBitOff(i, n) && i < 32)
            { i++; }
            while (IsBitOn(i, n) && i < 32)
            { i++; }
            n = TurnBitOn(i, n);
            n = TurnBitOff(i - 1, n);
            i = i - 1;
            var j = 0;
            while (j < i)
            {
                while (IsBitOff(i, n) && j < i)
                { i--; }
                n = TurnBitOff(i, n);
                while (IsBitOn(j, n) && j < i)
                { j++; }
                n = TurnBitOn(j, n);
                j++;
                i--;
            }
            return n;
        }

        private static int NextSmallest(int n)
        {
            var index = 0;
            var countZeros = 0;
            while (IsBitOn(index, n) && index < 32)
            { index++; }
            while (IsBitOff(index, n) && index < 32)
            {
                index++;
                countZeros++;
            }
            n = TurnBitOff(index, n);
            index--;
            n = TurnBitOn(index, n);
            countZeros--;
            for (var i = index - 1; i >= countZeros; i--)
            {
                n = TurnBitOn(i, n);
            }
            for (var i = countZeros - 1; i >= 0; i--)
            {
                n = TurnBitOff(i, n);
            }
            return n;
        }

        public static (int, int) GetNextSmallestAndNextLargestWithSame1BitsThatBrutForce(int n)
        {
            if (n == 0)
            { return (0, 0); }
            if (n + 1 == 0)
            { return (n, n); }
            if (n - 1 == 0)
            { return (n, n+1); }

            var count = Count(n);
            var largest = n + 1;
            var smallest = n - 1;
            while (Count(smallest) != count)
            { smallest--; }
            while (Count(largest) != count)
            { largest++; }
            return (smallest, largest);
        }

        public static (int, int) GetSmallestAndLargestNumberWithSame1BitsThat(int n)
        {
            var count = Count(n);
            if (count == 0)
            { return (0, 0); }

            var max = ~0;
            // all ones to the left is the greates possible
            var greatest = max - ((1 << (32 - count)) - 1);
            // all ones to the right is the smallest possible
            var smallest = (1 << count) - 1;
            return (smallest, greatest);
        }

        private static int Count(int n)
        {
            var mask = 1;
            var count = 0;
            while (n > 0)
            {
                if ((n & mask) == 1)
                {
                    count++;
                }
                n >>= 1;
            }
            return count;
        }
    }
}
