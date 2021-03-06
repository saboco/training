using Xunit;
using Training.HackerRank.Algorithms;
using Training.Tests.Common;

namespace Training.HackerRank.Tests.Algorithms
{
    public class BubbleSortTests
    {
        [Theory]
        [InlineData(new[] {3, 2, 1}, 3, 1, 3)]
        [InlineData(new[] {1, 2, 3}, 0, 1, 3)]
        [InlineData(new[] {1, 2, 3, 0, 5, 7, 4}, 5, 0, 7)]
        public void Should_sort_an_array(int[] a, int numSwaps, int firstElement, int lastElement)
        {
            var printer = new FakePrinter();
            var sort = new BubbleSort(printer);
            sort.Sort(a);
            AssertHelpers.AssertIsSorted(a);
            Assert.Equal($"Array is sorted in {numSwaps} swaps.", printer.Printed[0]);
            Assert.Equal($"First Element: {firstElement}", printer.Printed[1]);
            Assert.Equal($"Last Element: {lastElement}", printer.Printed[2]);
        }
    }
}