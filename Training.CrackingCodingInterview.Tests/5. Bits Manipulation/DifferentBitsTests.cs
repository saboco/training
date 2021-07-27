using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Training.CrackingCodingInterview.Tests
{
    public class DifferentBitsTests
    {
        List<Func<int, int, int>> _actions = new List<Func<int, int, int>>
        {
            DifferentBits.DifferentBitsBetween,
            DifferentBits.DifferentBitsBetween2
        };
        [Theory]
        [InlineData("11111", "1110", 2)]
        [InlineData("11111000", "10001110", 5)]
        public void DifferentBitsTest(string aB, string bB, int expected)
        {

            var a = Convert.ToInt32(aB, 2);
            var b = Convert.ToInt32(bB, 2);
            foreach (var action in _actions)
            {
                var count = action.Invoke(a, b);
                Assert.Equal(expected, count);
            }
        }

    }
}
