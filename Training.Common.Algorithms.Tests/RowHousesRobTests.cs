using System;
using System.Collections.Generic;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class RowHousesRobTests
    {
        List<Func<int[], (int, int[])>> _actions = new List<Func<int[], (int, int[])>>
        {
            //RowHousesRob.RobHouses,
            //RowHousesRob.RobHousesMemo,
            //RowHousesRob.RobHousesDp,
            RowHousesRob.RobHousesDp2
        };

        [Theory]
        [InlineData(new[] { 20, 25, 30, 15, 10 }, 60, new[] { 0, 2, 4 })]
        public void RobHousesTest(int[] houses, int expected, int[] expectedHouses)
        {
            foreach (var action in _actions)
            {
                var (rob, robbedHouses) = action.Invoke(houses);
                Assert.Equal(expected, rob);
                Common.AssertEqual(expectedHouses, robbedHouses);
            }
        }
    }
}

