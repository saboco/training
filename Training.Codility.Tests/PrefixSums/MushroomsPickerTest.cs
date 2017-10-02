using NUnit.Framework;

namespace Training.Codility.Tests.PrefixSums
{
    public class MushroomsPickerTest
    {
        [TestCase(new[] { 2, 3, 7, 5, 1, 3, 9 }, 6, 4, ExpectedResult = 25)]
        public int Should_return_the_maximal_number_of_picked_mushrooms_in_m_movements_starting_from_k(int[] mushrooms, int m, int k)
        {
            return Codility.PrefixSums.MushroomsPicker.Solution.Solve(mushrooms, m, k);
        }
    }
}