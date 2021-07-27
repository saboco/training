namespace Training.CrackingCodingInterview
{
    public class DifferentBits
    {
        public static int DifferentBitsBetween(int a, int b)
        {
            if (a == b)
            { return 0; }
            var count = 0;
            for (var i = 0; i < 32; i++)
            {
                var mask = Mask(i);
                if ((a & mask) != (b & mask))
                {
                    count++;
                }
            }
            return count;
        }

        private static int Mask(int index)
        { return 1 << index; }

        public static int DifferentBitsBetween2(int a, int b)
        { 
            var c = a ^ b;
            var mask = 1;
            var count = 0;
            while(c > 0)
            { 
                if((c & mask) == 1)
                { 
                    count++;
                }
                c = c >> 1;
            }
            return count;
        }
    }
}
