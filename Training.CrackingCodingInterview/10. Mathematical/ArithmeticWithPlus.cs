using System;

namespace Training.CrackingCodingInterview
{
    public class ArithmeticWithPlus
    {
        public static int Substract(int a, int b)
        {
            return a + Negate(b);
        }

        private static int Negate(int n)
        {
            var unitOfAddition = n < 0 ? 1 : -1;
            var result = 0;
            while (n != 0)
            {
                result += unitOfAddition;
                n += unitOfAddition;
            }
            return result;
        }

        public static int Multiply(int a, int b)
        {
            if (a == 0 || b == 0)
            { return 0; }

            if (a < b)
            { return Multiply(b, a); }

            var sum = 0;
            var times = Abs(b);
            for (var i = 0; i < times; i++)
            {
                sum += a;
            }

            return b < 0 ? Negate(sum) : sum;
        }

        private static int Abs(int n)
        {
            return n < 0 ? Negate(n) : n;
        }

        public static int Divide(int a, int b)
        {
            if (b == 0)
            {
                throw new InvalidOperationException("Division by 0 is not defined");
            }

            var quotient = 0;
            var isNegative = (a < 0 && b > 0) || (a > 0 && b < 0);
            var divisor = Negate(Abs(b));
            var dividend = Abs(a);
            while (dividend >= Abs(divisor))
            {
                dividend += divisor;
                quotient++;
            }
            return isNegative ? Negate(quotient) : quotient;
        }
    }
}
