using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTicTacToe
{
    public class Engine
    {
        public TicTacToeMove GetMove(char[,] board, char player, char aiPlayer)
        {
            MoveState state = GetMoveState(board, player, aiPlayer);

            switch(state)
            {
                case MoveState.NullGame:
                    return CreateMove(board, player, aiPlayer, 0, 0);
                case MoveState.Playing:
                    return GetNextMove(board, player, aiPlayer);
                default:
                    return new TicTacToeMove(board, state: state);
            }
        }

        public MoveState GetMoveState(char[,] board, char player, char aiPlayer)
        {
            // Check for Empty game
            if (Flatten(board).All(ch => ch == '\0'))
                return MoveState.NullGame;

            foreach (char ch in GetWinnerForLine(board))
            {
               if (ch == player) return MoveState.PlayerWin;
                if (ch == aiPlayer) return MoveState.AiWin;
            }

            // Check for Cat game
            if (Flatten(board).All(ch => ch != '\0'))
                return MoveState.CatGame;

            return MoveState.Playing;
        }

        public IEnumerable<char> GetWinnerForLine(char[,] board)
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
        
            yield return FindWinner(board, order, order-1, 0, -1, 1);
        }

        public static char FindWinner(char[,] board, int order, int startRow, int startCol, int dr, int dc)
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

        public static IEnumerable<char> Flatten(char[,] board)
        {
            int order = GetOrder(board);

            for (int i = 0; i < order; i++)
                for (int j = 0; j < order; j++)
                    yield return board[i, j];
        }

        Random rnd = new Random();

        private TicTacToeMove GetNextMove(char[,] board, char player, char aiPlayer)
        {
            int order = GetOrder(board);

            int row, col;

            for (;;)
            {
                row = rnd.Next(order);
                col = rnd.Next(order);
                char cell = board[row, col];

                if (cell == '\0')
                    return CreateMove(board, player, aiPlayer, row, col);
            }
        }

        private TicTacToeMove CreateMove(char[,] board, char player, char aiPlayer, int row, int col)
        {
            char[,] newBoard = (char[,])board.Clone();
            newBoard[row, col] = aiPlayer;
            MoveState state = GetMoveState(newBoard, player, aiPlayer);
            return new TicTacToeMove(newBoard, row, col, state);
        }

        public static int GetOrder(char[,] board)
        {
            if (board == null) return 0;
            return Math.Min(board.GetUpperBound(0), board.GetUpperBound(1)) + 1;
        }

    }
}
