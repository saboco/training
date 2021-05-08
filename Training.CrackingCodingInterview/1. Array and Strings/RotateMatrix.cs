namespace Training.CrackingCodingInterview
{
    public class RotateMatrix
    {
        private static void Rotate(int[][] m, int n, int x, int y)
        {
            var tmp1 = m[y][x];
            for(var i=0; i < 4; i++)
            { 
                var xp = n - y;
                var yp = x;
                var tmp2 = m[yp][xp];
                m[yp][xp]=tmp1;
                x=xp;
                y=yp;
                tmp1 = tmp2;
            }
        }

        public static void Rotate(int[][] m)
        {
            var n = m.Length - 1;
            for(var i = 0; i < n -i;i++)
            {
                for(var j=i;j<n-i;j++)
                {
                    Rotate(m,n,i,j);
                }
            }
        }
    }
}
