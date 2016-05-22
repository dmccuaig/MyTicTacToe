using System;
using System.Linq;
using TicTacToe.Model;

namespace TicTacToe.Engine
{
    public partial class AiEngine
    {
        private ConsoleView.ConsoleView _consoleView = new ConsoleView.ConsoleView();

        public TicTacToeMove GetMove(char[,] board, char player, char aiPlayer)
        {
            MoveState state = GetMoveState(board, player, aiPlayer);

            switch (state)
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

            char winner = GetWinner(board);
            if (winner == player) return MoveState.PlayerWin;
            if (winner == aiPlayer) return MoveState.AiWin;

            // Check for Cat game
            if (Flatten(board).All(ch => ch != '\0'))
                return MoveState.CatGame;

            return MoveState.Playing;
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

        //private TicTacToeMove GetNextMove(char[,] board, char player, char aiPlayer)
        //{
        //    int order = GetOrder(board);

        //    for (;;)
        //    {
        //        int row = _rnd.Next(order);
        //        int col = _rnd.Next(order);
        //        char cell = board[row, col];

        //        if (cell == '\0')
        //            return CreateMove(board, player, aiPlayer, row, col);
        //    }
        //}

        private TicTacToeMove GetNextMove(char[,] board, char player, char aiPlayer)
        {
            int order = GetOrder(board);

            for (;;)
            {
                int row = _rnd.Next(order);
                int col = _rnd.Next(order);
                char cell = board[row, col];

                if (cell == '\0')
                    return CreateMove(board, player, aiPlayer, row, col);
            }
        }

    }
}
