using Training.Common;

namespace Training.HackerRank.Algorithms
{
    public class BubbleSort
    {
        private readonly IPrint _printer;

        public BubbleSort(IPrint printer)
        {
            _printer = printer;
        }

        public void Sort(int[] a)
        {
            var n = a.Length;
            var totalNumSwaps = 0;

            for (var i = 0; i < n; i++)
            {
                // Track number of elements swapped during a single array traversal
                var numberOfSwaps = 0;
                
                for (var j = 0; j < n - 1; j++)
                {
                    // Swap adjacent elements if they are in decreasing order
                    if (a[j] <= a[j + 1]) continue;
                    ArrayHelpers.Swap(a, j, j + 1);
                    numberOfSwaps++;
                    totalNumSwaps++;
                }

                // If no elements were swapped during a traversal, array is sorted
                if (numberOfSwaps == 0)
                {
                    break;
                }
            }

            _printer.Print($"Array is sorted in {totalNumSwaps} swaps.");
            _printer.Print($"First Element: {a[0]}");
            _printer.Print($"Last Element: {a[n - 1]}");
        }
    }
}