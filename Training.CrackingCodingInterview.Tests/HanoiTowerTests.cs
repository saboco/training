using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class HanoiTowerTests
    {
        [Fact]
        public void HanoiTower_3Disks()
        {
            var game = new HanoiTower(3);
            game.Solve();
        }

        [Fact]
        public void HanoiTower_4Disks()
        {
            var game = new HanoiTower(4);
            game.Solve();
        }

        [Fact]
        public void HanoiTower_5Disks()
        {
            var game = new HanoiTower(5);
            game.Solve();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void HanoiTowerTest(int disks)
        {
            var game = new HanoiTower(disks);
            game.Solve();
        }
    }
}
