using NUnit.Framework;
using Training.HackerRank.DataStructures;

namespace Training.HackerRank.Tests.DataStructures
{
    public class ConnectedCellsInAGridTest
    {
        [Test]
        public void Should_return_the_greates_region_when_asked_for_it()
        {
            var grid = new[] { new[] { 1, 1, 0, 0 }, new[] { 0, 1, 1, 0 }, new[] { 0, 0, 1, 0 }, new[] { 1, 0, 0, 0 } };
            var sut = new ConnectedCellsInAGrid(grid);
            var greatesArea = sut.GetGreatesRegion();
            Assert.AreEqual(5, greatesArea);
        }

        [Test]
        public void Should_return_the_greates_region_when_asked_for_it_2()
        {
            var grid = new[]
            {
                new[] {0, 1, 0, 0, 0, 0, 1, 1, 0},
                new[] {1, 1, 0, 0, 1, 0, 0, 0, 1},
                new[] {0, 0, 0, 0, 1, 0, 1, 0, 0},
                new[] {0, 1, 1, 1, 0, 1, 0, 1, 1},
                new[] {0, 1, 1, 1, 0, 0, 1, 1, 0},
                new[] {0, 1, 0, 1, 1, 0, 1, 1, 0},
                new[] {0, 1, 0, 0, 1, 1, 0, 1, 1},
                new[] {1, 0, 1, 1, 1, 1, 0, 0, 0}
            };

            var sut = new ConnectedCellsInAGrid(grid);
            var greatesArea = sut.GetGreatesRegion();
            Assert.AreEqual(29, greatesArea);
        }

        [Test]
        public void Should_return_the_greates_region_when_asked_for_it_3()
        {
            var grid = new[]
            {
                new []{1, 0, 0, 1, 0, 1, 0, 0},
                new []{0, 0, 0, 0, 0, 0, 0, 1},
                new []{1, 0, 1, 0, 1, 0, 0, 0},
                new []{0, 0, 0, 0, 0, 0, 1, 0},
                new []{1, 0, 0, 1, 0, 0, 0, 0},
                new []{0, 0, 0, 0, 0, 0, 0, 1},
                new []{0, 1, 0, 0, 0, 1, 0, 0}
            };

            var sut = new ConnectedCellsInAGrid(grid);
            var greatesArea = sut.GetGreatesRegion();
            Assert.AreEqual(1, greatesArea);
        }
    }
}