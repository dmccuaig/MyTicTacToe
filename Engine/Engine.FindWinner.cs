using System.Collections.Generic;

namespace TicTacToe.Engine
{
    partial class AiEngine
    {
        private char GetWinner(char[,] board)
        {
            foreach (char ch in GetWinnerForLine(board))
            {
                if (ch != '\0') return ch;
            }

            return '\0';
        }

        private IEnumerable<char> GetWinnerForLine(char[,] board)
        {
            int order = GetOrder(board);

            for (int i = 0; i < order; i++)
            {
                // Check row
                yield return FindWinner(board, order, i, 0, 0, 1);

                // Check row
                yield return FindWinner(board, order, 0, i, 1, 0);
            }

            // Check diagonals - top left to bottom right
            yield return FindWinner(board, order, 0, 0, 1, 1);

            // Check diagonals - bottom left to top right

            yield return FindWinner(board, order, order - 1, 0, -1, 1);
        }

        private static char FindWinner(char[,] board, int order, int startRow, int startCol, int dr, int dc)
        {
            char firstField = board[startRow, startCol];
            for (int i = 0; i < order; i++)
            {
                int r = startRow + dr * i;
                int c = startCol + dc * i;

                char cell = board[r, c];
                if (cell != firstField) return '\0';
            }

            return firstField;
        }
    }
}
