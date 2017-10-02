using NUnit.Framework;

namespace Training.Tests.Common
{
    public static class AssertHelpers
    {
        public static void AssertIsSorted(int[] arr)
        {
            for (var i = 1; i < arr.Length; i++)
            {
                Assert.IsTrue((arr[i - 1] - arr[i]) < 1);
            }
        }
    }
}