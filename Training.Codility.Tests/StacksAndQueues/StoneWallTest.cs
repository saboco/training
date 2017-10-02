using NUnit.Framework;

namespace Training.Codility.Tests.StacksAndQueues
{
    public class StoneWallTest
    {
         [TestCase(new []{8,8,5,7,9,8,7,4,8}, ExpectedResult = 7)]
         public int Should_return_the_minimum_number_of_rectagles_to_cover_manhattan_skyline(int[] a)
         {
             return Codility.StacksAndQueues.StoneWall.Solution.Solve(a);
         }
    }
}