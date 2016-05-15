using System;
using System.Collections.Generic;
using System.Linq;
using Model;

namespace MyEngine
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

        #region Determine board state

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
        
            yield return FindWinner(board, order, order-1, 0, -1, 1);
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

        private static IEnumerable<char> Flatten(char[,] board)
        {
            int order = GetOrder(board);

            for (int i = 0; i < order; i++)
                for (int j = 0; j < order; j++)
                    yield return board[i, j];
        }

        private static int GetOrder(char[,] board)
        {
            if (board == null) return 0;
            return Math.Min(board.GetUpperBound(0), board.GetUpperBound(1)) + 1;
        }

        #endregion

        private TicTacToeMove CreateMove(char[,] board, char player, char aiPlayer, int row, int col)
        {
            char[,] newBoard = (char[,])board.Clone();
            newBoard[row, col] = aiPlayer;
            MoveState state = GetMoveState(newBoard, player, aiPlayer);
            return new TicTacToeMove(newBoard, moveRow: row, moveCol: col, state: state);
        }

        private readonly Random _rnd = new Random();

        private TicTacToeMove GetNextMove(char[,] board, char player, char aiPlayer)
        {
            int order = GetOrder(board);

            int[,] minMaxBoard = new int[order, order];

            int row, col;

            for (;;)
            {
                row = _rnd.Next(order);
                col = _rnd.Next(order);
                char cell = board[row, col];

                if (cell == '\0')
                    return CreateMove(board, player, aiPlayer, row, col);
            }
        }

        //private int GetMinimax(char[,] board, int order, char player, char aiPlayer)
        //{
        //    var moveState = GetMoveState(board, player, aiPlayer);
        //    if (moveState == MoveState.AiWin) return 100;
        //    if (moveState == MoveState.PlayerWin) return -100;

        //    {
        //        scoreboard = null;
        //        return true;
        //    }

        //}

        /*

        private MoveState EvaluateBoard(char[,] board, int order, char player, char aiPlayer, out int[,] scoreboard)
        {
            var moveState = GetMoveState(board, player, aiPlayer);
            if (moveState == MoveState.PlayerWin || moveState == MoveState.AiWin)
            {
                scoreboard = null;
                return true;
            }

            scoreboard = new int[order, order];

            for (int i = 0; i < order; i++)
            {
                for (int j = 0; j < order; j++)
                {
                    char cell = board[i, j];
                    if (cell != '\0') continue;





                }

            }
            return false;
        }

     */

            /*
        public static RowCol GetBestMove(char[,] gb, char p)
        {
            RowCol? bestSpace = null;
            List<RowCol> openSpaces = new List<RowCol>();

            int order = GetOrder(gb);

            for (int i = 0; i < order; i++)
            {
                for (int j = 0; j < order; j++)
                {
                    if (gb[i, j] == '\0')
                        openSpaces.Add(new RowCol(i, j));
                }
            }

            char[,] newBoard;

            for (int i = 0; i < openSpaces.Count; i++)
            {
                newBoard = (char[,])gb.Clone();
                RowCol newSpace = openSpaces[i];

                newBoard[newSpace.Row, newSpace.Col] = p;

                if (newBoard.Winner == Player.Open && newBoard.OpenSquares.Count > 0)
                {
                    RowCol tempMove = GetBestMove(newBoard, ((Player)(-(int)p)));  //a little hacky, inverts the current player
                    newSpace.Rank = tempMove.Rank;
                }
                else
                {
                    if (newBoard.Winner == Player.Open)
                        newSpace.Rank = 0;
                    else if (newBoard.Winner == Player.X)
                        newSpace.Rank = -1;
                    else if (newBoard.Winner == Player.O)
                        newSpace.Rank = 1;
                }

                //If the new move is better than our previous move, take it
                if (bestSpace == null ||
                   (p == Player.X && newSpace.Rank < ((Space)bestSpace).Rank) ||
                   (p == Player.O && newSpace.Rank > ((Space)bestSpace).Rank))
                {
                    bestSpace = newSpace;
                }
            }

            return (RowCol)bestSpace;
        }

    */
        struct RowCol
        {
            public RowCol(int row, int col)
            {
                Row = row;
                Col = col;
            }

            public int Row;
            public int Col;
        }
    }
}
