using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class Common
    {

        public static void AssertEqual(int[][] expected, int[][] actual)
        {
            Assert.Equal(expected.Length, actual.Length);

            for (var i = 0; i < expected.Length; i++)
            {
                AssertEqual(expected[i], actual[i]);
            }
        }
        public static void AssertEqual(int[] expected, int[] actual)
        {
            Assert.Equal(expected.Length, actual.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
        public static void AssertEqual(string[][] expected, string[][] actual)
        {
            Assert.Equal(expected.Length, actual.Length);

            for (var i = 0; i < expected.Length; i++)
            {
                AssertEqual(expected[i], actual[i]);
            }
        }
        public static void AssertEqual(string[] expected, string[] actual)
        {
            Assert.Equal(expected.Length, actual.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
        public static void AssertEqual((int,int)[] expected, (int,int)[] actual)
        {
            Assert.Equal(expected.Length, actual.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
    }
}
