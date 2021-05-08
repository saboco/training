namespace Training.CrackingCodingInterview
{
    public class StringIsARotation
    {
        public static bool IsARotation(string s1, string s2)
        {
            if (s1 == null || s2 == null)
            { return false; }

            var ss = s2 + s2;
            return ss.IndexOf(s1) >= 0;
        }
    }
}
