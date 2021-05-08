using System.Text;

namespace Training.CrackingCodingInterview
{
    public class ReplaceSpaces
    {
        // Time: O(n)
        // Space: O(n)
        public static string ScapeSpaces(string s)
        {
            if (s == null || s == "")
            { return s; }

            var sb = new StringBuilder();
            foreach (var c in s)// O(n)
            {
                if (c == ' ')
                { sb.Append("%20"); }
                else
                { sb.Append(c); }
            }
            return sb.ToString(); // O(n)
        }

        public static string Replace(string s)
        {
            return s?.Replace(" ", "%20");
        }
    }
}
