using System.Collections.Generic;

namespace Training.Common
{
    public static class StringHelpers
    {
        public static Dictionary<string, int> ToAggregatedDictionary(this string[] strArr)
        {
            var dic = new Dictionary<string, int>();
            for (var i = 0; i < strArr.Length; i++)
            {
                if (!dic.ContainsKey(strArr[i]))
                {
                    dic.Add(strArr[i], 1);
                }
                else
                {
                    dic[strArr[i]]++;
                }
            }
            return dic;
        }
    }
}