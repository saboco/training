using System;
using System.Collections.Generic;

namespace Training.Common
{
    public static class FuncExtentions
    {
        public static Func<T1, T2> Memoize<T1, T2>(this Func<T1, T2> func)
        {
            var dic = new Dictionary<T1, T2>();
            
            return (T1 n) =>
            {
                T2 result;
                if (dic.TryGetValue(n, out result)) return result;

                result = func(n);
                dic.Add(n, result);
                return result;
            };
        }
    }
}
