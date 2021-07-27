using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Common.Algorithms
{
    public class NQueens
    {
        public static bool SolveNQueens(int n)
        {
            var board = new int[n][];
            for (var i = 0; i < n; i++)
            {
                board[i] = new int[n];
            }

            return CanPosition(n, board, 0);
        }

        private static bool CanPosition(int n, int[][] board, int k)
        {
            if (n == 0)
            { return true; }

            for (var i = k; i < board.Length; i++)
            {
                for (var j = 0; j < board.Length; j++)
                {
                    if (!Attacking(board, i, j))
                    {
                        board[i][j] = 1;
                        if (CanPosition(n - 1, board, i + 1))
                        { return true; }
                        board[i][j] = 0;
                    }
                }
            }

            return false;
        }

        private static bool Attacking(int[][] board, int k, int l)
        {
            for (var i = 0; i < board.Length; i++)
            {
                if (board[i][l] == 1)
                {
                    return true;
                }
            }
            for (var i = 0; i < board.Length; i++)
            {
                if (k + i < board.Length && l + i < board.Length && board[k + i][l + i] == 1)
                { return true; }
                if (k - i >= 0 && l - i >= 0 && board[k - i][l - i] == 1)
                { return true; }
                if (k + i < board.Length && l - i >= 0 && board[k + i][l - i] == 1)
                { return true; }
                if (k - i >= 0 && l + i < board.Length && board[k - i][l + i] == 1)
                { return true; }
            }
            return false;
        }
    }
}
