using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class RemoveDuplicateCharactersTests
    {
        [Theory]
        [InlineData(new char[] { 'a', 'a', 'a', '\0' }, new char[] { 'a', '\0' })]
        [InlineData(new char[] { 'a', '\0' }, new char[] { 'a', '\0' })]
        [InlineData(new char[0], new char[0])]
        [InlineData(null, null)]
        public void RemoveDuplicateCharactersInPlaceTest(char[] s, char[] expected)
        {
            var actual = RemoveDuplicateCharacters.RemoveDuplicateCharactersInPlace(s);
            if (expected != null)
            {
                for (var i = 0; i < expected.Length; i++)
                {
                    Assert.Equal(expected[i], actual[i]);
                }
            }
        }
    }
}
