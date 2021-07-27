using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.CrackingCodingInterview
{
    public class SwapBits
    {
        public static int SwapOddEvenBits(int x)
        {
            var xL = (x << 1) & 0xAAAA;
            var xR = (x >> 1) & 0x5555;
            return (xL | xR);
        }
    }
}
