namespace Training.Leetcode
{
    public class ImplementStrStr
    {
        public static int StrStr(string haystack, string needle)
        {
            if (haystack == null)
            {
                haystack = "";
            }
            if (needle == null || needle.Length == 0)
            {
                return 0;
            }
            if (needle.Length > haystack.Length)
            {
                return -1;
            }

            int j = 0, l = needle.Length, n = haystack.Length;
            for (var i = 0; i < n - l + 1; i++)
            {
                if (haystack[i] != needle[0])
                {
                    continue;
                }
                var indexOf = i;
                while (j < l && i < n && haystack[i] == needle[j])
                {
                    j++;
                    i++;
                }
                if (j == l)
                {
                    return indexOf;
                }
                i = indexOf;
                j = 0;
            }
            return -1;
        }

        public static int StrStr2(string haystack, string needle)
        {
            if (haystack == null)
            {
                haystack = "";
            }
            if (needle == null || needle.Length == 0)
            {
                return 0;
            }
            if (needle.Length > haystack.Length)
            {
                return -1;
            }

            int l = needle.Length, n = haystack.Length;
            for (var i = 0; i < n - l + 1; i++)
            {
                if (haystack.Substring(i, l) == needle)
                { return i; }
            }
            return -1;
        }

        public static int KMP(string haystack, string needle)
        {
            if (haystack == null)
            {
                haystack = "";
            }
            if (needle == null || needle.Length == 0)
            {
                return 0;
            }
            if (needle.Length > haystack.Length)
            {
                return -1;
            }

            var lps = LpsTable(needle);
            int i = 0, j = 0;
            while (i < haystack.Length && j < needle.Length)
            {
                if (haystack[i] == needle[j])
                {
                    j++;
                    i++;
                }

                if (j == needle.Length)
                {
                    return i - j;
                }
                else if (i < haystack.Length && haystack[i] != needle[j])
                {
                    if (j - 1 >= 0)
                    {
                        j = lps[j - 1];
                    }
                    else
                    { i++; }
                }
            }

            return -1;
        }

        public static int[] LpsTable(string pattern)
        {
            var lps = new int[pattern.Length];
            int i = 1, n = lps.Length, len = 0;
            lps[0] = 0;
            while (i < n)
            {
                if (pattern[i] == pattern[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else
                {
                    if (len != 0)
                    {
                        len = lps[len - 1];
                    }
                    else
                    {
                        lps[i] = 0;
                        i++;
                    }
                }
            }
            return lps;
        }
    }
}