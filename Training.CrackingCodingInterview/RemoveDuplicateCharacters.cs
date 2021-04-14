namespace Training.CrackingCodingInterview
{
    public class RemoveDuplicateCharacters
    {
        // Time: O(n^2)
        // Space: O(1)
        public static char[] RemoveDuplicateCharactersInPlace(char[] s)
        {
            if (s == null)
            { return s; }

            for (var i = 0; i < s.Length; i++) // O (n)
            {
                for (var j = i + 1; j < s.Length; j++) // * O (n)
                {
                    if (s[i] == s[j])
                    {
                        s[j] = '\0';
                    }
                }
            }
            for (var i = 0; i < s.Length; i++) // O(n)
            {
                if (s[i] == '\0' && i + 1 < s.Length)
                {
                    s[i] = s[i + 1];
                }
            }
            return s;
        }
    }
}
