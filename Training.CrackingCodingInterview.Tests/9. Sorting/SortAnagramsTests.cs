using System;
using System.Collections.Generic;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class SortAnagramsTests
    {
        readonly List<Action<string[]>> _actions = new List<Action<string[]>>
        {
            SortAnagrams.SimplestSort,
            SortAnagrams.Sort
        };

        [Theory]
        [InlineData(
            new[] { "arroz", "hola", "zorra", "gata", "cata", "taga", "maga", "cate" },
            new[] { "cata", "maga", "gata", "taga", "cate", "hola", "arroz", "zorra" })]
        [InlineData(
            new[] { "arroz", "hola", "zorra", "cate", "cata", "taga", "maga", "gata" },
            new[] { "cata", "maga", "taga", "gata", "cate", "hola", "arroz", "zorra" })]
        public void SortAnagramsTest(string[] anagrams, string[] expected)
        {
            foreach (var action in _actions)
            {
                var arr = new string[anagrams.Length];
                Array.Copy(anagrams, arr, anagrams.Length);
                action.Invoke(arr);
                Common.AssertEqual(expected, arr);
            }
        }
    }
}
