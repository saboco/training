namespace Training.CrackingCodingInterview
{
    public class InsertBetween
    {
        public static int InsertBitsBetween(int n, int m, int i, int j)
        {
            var mask = 1;
            for (var p = 32; p >= 0; p--)
            {
                if (p >= i && p <= j)
                {
                    mask = mask << 1;
                }
                else
                {
                    mask = (mask << 1) + 1;
                }
            }
            
            return (n & mask) | (m << i);
        }

        public static int InsertBitsBetween2(int n, int m, int i, int j)
        {
            var max = ~0;
            var left = max - ((1 << j) - 1);
            var right = (1 << i) - 1;
            var mask = left | right;
            
            return (n & mask) | (m << i);
        }
    }
}
