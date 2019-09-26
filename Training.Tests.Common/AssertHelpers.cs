using System;
using Xunit;

namespace Training.Tests.Common
{
    public static class AssertHelpers
    {
        public static void AssertIsSorted(int[] arr)
        {
            for (var i = 1; i < arr.Length; i++)
            {
                Assert.True((arr[i - 1] - arr[i]) < 1);
            }
        }

        public static void DoesNotThrow(Action f)
        {
            var doesNotThrow = true;
            try
            {
                f();
            }
            catch
            {
                doesNotThrow = false;
            }
            Assert.True(doesNotThrow);
        }
    }
}