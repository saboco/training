using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Training.Common.Algorithms.Tests
{
    public class DungeonScapeTests
    {
        [Theory]
        [InlineData(9,
            new[] { 'S', '.', '.', '#', '.', '.', '.' },
            new[] { '.', '#', '.', '.', '.', '#', '.' },
            new[] { '.', '#', '.', '.', '.', '.', '.' },
            new[] { '.', '.', '#', '#', '.', '.', '.' },
            new[] { '#', '.', '#', 'E', '.', '#', '.'  })]
        public void ShortScapeWayTest(int expected, params char[][] grid)
        {
            var count = DungeonScape.ShortestWayOut(ArrayHelpers.ToMatrix(grid));
            Assert.Equal(expected, count);
        }
    }
}
