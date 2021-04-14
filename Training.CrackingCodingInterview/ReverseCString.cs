namespace Training.CrackingCodingInterview
{
    public class ReverseCString
    {
        public static char[] Reverse(char[] s)
        {
            if (s == null || s.Length < 2)
            { return s; }
            var reversed = new char[s.Length];
            for (int i = s.Length - 2, j = 0; i >= 0; i--, j++)
            {
                reversed[j] = s[i];
            }
            reversed[s.Length - 1] = '\0';
            return reversed;
        }

        public static char[] ReverseWhile(char[] s)
        {
            if (s == null || s.Length < 2)
            { return s; }
            var reversed = new char[s.Length];
            reversed[s.Length - 1] = '\0';
            var i = 0;
            var j = s.Length - 2;
            while (s[i] != '\0')
            {
                reversed[j] = s[i];
                i++;
                j--;
            }
            return reversed;
        }
    }
}
