using System;
using System.Collections.Generic;

namespace Training.Leetcode
{
    public class BackSpaceStringCompare
    {
        public static bool BackspaceCompare0(string S, string T)
        {
            int readS = 0, writeS = 0;
            var s = S.ToCharArray();
            while (readS < s.Length)
            {
                if (s[readS] == '#')
                {
                    writeS = Math.Max(0, writeS - 1);
                }
                else
                {
                    s[writeS] = s[readS];
                    writeS++;
                }
                readS++;
            }
            int readT = 0, writeT = 0;
            var t = T.ToCharArray();
            while (readT < t.Length)
            {
                if (t[readT] == '#')
                {
                    writeT = Math.Max(0, writeT - 1);
                }
                else
                {
                    t[writeT] = t[readT];
                    writeT++;
                }
                readT++;
            }
            if (writeT != writeS)
                return false;
            for (var i = 0; i < writeT; i++)
            {
                if (s[i] != t[i])
                    return false;
            }
            return true;
        }

        public static bool BackspaceCompare(string S, string T)
        {
            IEnumerable<char> Next(string s)
            {
                var skip = 0;
                for (var i = s.Length - 1; i >= 0; i--)
                {
                    if (s[i] == '#')
                    { skip++; }
                    else if (skip > 0)
                    { skip--; }
                    else
                    {
                        yield return s[i];
                    }
                }
                yield return '\0';
            }
            IEnumerable<(char, char)> Zip(string s, string t)
            {
                var sEnumerator = Next(s).GetEnumerator();
                var tEnumerator = Next(t).GetEnumerator();
                do
                {
                    tEnumerator.MoveNext();
                    sEnumerator.MoveNext();
                    yield return (sEnumerator.Current, tEnumerator.Current);
                }
                while (sEnumerator.Current != '\0' && tEnumerator.Current != '\0');
            }

            foreach (var (x, y) in Zip(S, T))
            {
                if (x != y)
                { return false; }
            }
            return true;
        }
    }
}
