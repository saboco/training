using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.CrackingCodingInterview
{
    public class Screen
    {
        public static void FillIn(int[][] screen, (int, int) point, int newColor)
        {
            var (x, y) = point;
            var currentColor = screen[x][y];
            FillIn(screen, currentColor, newColor, x, y);
            screen[x][y] = newColor;
        }
        private static void FillIn(int[][] screen, int currentColor, int newColor, int x, int y)
        {
            if (x + 1 < screen.Length && screen[x + 1][y] == currentColor)
            {
                screen[x + 1][y] = newColor;
                FillIn(screen, currentColor, newColor, x + 1, y);
            }
            if (x - 1 > 0 && screen[x - 1][y] == currentColor)
            {
                screen[x - 1][y] = newColor;
                FillIn(screen, currentColor, newColor, x - 1, y);
            }
            if (y + 1 < screen[x].Length && screen[x][y + 1] == currentColor)
            {
                screen[x][y + 1] = newColor;
                FillIn(screen, currentColor, newColor, x, y + 1);
            }
            if (y - 1 > 0 && screen[x][y - 1] == currentColor)
            {
                screen[x][y - 1] = newColor;
                FillIn(screen, currentColor, newColor, x, y - 1);
            }
        }
    }
}
