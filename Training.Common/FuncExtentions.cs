using System;
using System.Collections.Generic;

namespace Training.Common
{
    public static class FuncExtentions
    {
        public static Func<T1, TResult> Memoize<T1, TResult>(this Func<T1, TResult> func)
        {
            var dic = new Dictionary<T1, TResult>();

            return (T1 n) =>
            {
                if (dic.TryGetValue(n, out var result)) return result;

                result = func(n);
                dic.Add(n, result);
                return result;
            };
        }
    }
}