using System.Collections.Generic;
using System.Text;

namespace Training.CrackingCodingInterview
{
    public class EightQueens
    {
        public static List<string> PrintHappyQueens()
        {
            var board = new bool[8, 8];
            var queens = 8;
            var validBoards = new List<string>();
            PositionQueens(queens, board, validBoards, 0);
            return validBoards;
        }

        private static void PositionQueens(int queens, bool[,] board, List<string> validBoards, int row)
        {
            if (queens == 0)
            {
                validBoards.Add(PrintBoard(board));
            }

            for (var i = row; i < board.GetLength(0); i++)
            {
                for (var j = 0; j < board.GetLength(1); j++)
                {
                    if (CanPosition(board, i, j))
                    {
                        board[i, j] = true;
                        PositionQueens(queens - 1, board, validBoards, i + 1);
                        board[i, j] = false;
                    }
                }
            }
        }

        private static bool CanPosition(bool[,] board, int k, int l)
        {
            for (var i = 0; i < board.GetLength(0); i++)
            {
                if (board[i,l])
                {
                    return false;
                }
            }
            for (var i = 0; i < board.GetLength(0); i++)
            {
                if (k + i < board.GetLength(0) && l + i < board.GetLength(1) && board[k + i,l + i])
                { return false; }
                if (k - i >= 0 && l - i >= 0 && board[k - i,l - i])
                { return false; }
                if (k + i < board.GetLength(0) && l - i >= 0 && board[k + i,l - i])
                { return false; }
                if (k - i >= 0 && l + i < board.GetLength(1) && board[k - i,l + i])
                { return false; }
            }
            return true;
        }

        private static string PrintBoard(bool[,] board)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < board.GetLength(0); i++)
            {
                for (var j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j])
                    {
                        sb.Append("| Q |");
                    }
                    else
                    {
                        sb.Append("| · |");
                    }
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
