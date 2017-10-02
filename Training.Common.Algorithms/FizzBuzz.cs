namespace Training.Common.Algorithms
{
    public class FizzBuzz
    {
        private const int FizzMultiple = 3;
        private const int BuzzMultiple = 5;
        private const string FizzValue = "Fizz";
        private const string BuzzValue = "Buzz";
        private const string FizzBuzzValue = "FizzBuzz";

        private static bool IsBuzz(int n) => n % BuzzMultiple == 0;
        private static bool IsFizz(int n) => n % FizzMultiple == 0;

        public static string GetFizzBuzz(int number)
        {
            if (IsFizz(number) && IsBuzz(number)) return FizzBuzzValue;
            if (IsFizz(number)) return FizzValue;
            return IsBuzz(number) ? BuzzValue : number.ToString();
        }

        public static string GetFizzBuzzPatternMatching(int number)
        {
            switch (number)
            {
                case int n when IsFizz(n) && IsBuzz(n): return FizzBuzzValue;
                case int n when IsBuzz(n): return BuzzValue;
                case int n when IsFizz(n): return FizzValue;
                default: return number.ToString();
            }
        }
    }
}