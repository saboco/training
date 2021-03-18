using System.Linq;

namespace Training.Leetcode
{
    public class ReverseWordsInAStringIII
    {
        public static string ReverseWords(string s)
        {
            var words= s.Split(' ');            
            return string.Join(" ", words.Select(w=> string.Join("", w.Reverse())));
        }
    }
}
