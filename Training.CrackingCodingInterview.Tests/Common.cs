using Xunit;

namespace Training.CrackingCodingInterview.Tests
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

        public static void AssertEqual(string[] expected, string[] actual)
        {
            Assert.Equal(expected.Length, actual.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
        public static void AssertIsSorted(int[] arr)
        {
            var sorted = true;
            if (arr.Length > 1)
            {
                var prev = arr[0];
                for (var i = 1; i < arr.Length; i++)
                {
                    if (arr[i] < prev)
                    {
                        sorted = false;
                        break;
                    }
                    prev = arr[i];
                }
            }
            Assert.True(sorted);
        }
    }
}
