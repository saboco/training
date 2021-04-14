using Xunit;
using System;
using System.Collections.Generic;
namespace Training.CrackingCodingInterview.Tests
{
    public class AreAllCharacterUniqueInStringTests
    {
        List<Func<string, bool>> _actions = new List<Func<string, bool>>
        {
            AreAllCharactersUniqueInString.AreAllCharactersUnique,
            AreAllCharactersUniqueInString.AreAllUniqueWithoutOtherDataStructure,
            AreAllCharactersUniqueInString.AreAllUniqueSorting
        };

        [Theory]
        [InlineData("abcd", true)]
        [InlineData("abcda", false)]
        [InlineData("", true)]
        [InlineData(null, true)]
        public void Test1(string s, bool expected)
        {
            foreach (var action in _actions)
            {
                var actual = action.Invoke(s);
                Assert.Equal(expected, actual);
            }
        }
    }
}
