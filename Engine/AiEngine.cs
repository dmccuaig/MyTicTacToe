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
                    return GetNextMove2(board, player, aiPlayer);
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

        private TicTacToeMove GetNextMove2(char[,] board, char player, char aiPlayer)
        {
            _consoleView.ShowBoard(board, player, aiPlayer);

            Cell? maxCell = null;
            int maxScore = 0;

            foreach (Cell cell in OpenCells(board))
            {
                if (maxCell == null)
                {
                    maxCell = cell;
                    maxScore = 1;
                }

                int score = GetScoreForMove(board, aiPlayer, player, cell, 0);

                if (score > maxScore)
                {
                    maxCell = cell;
                    maxScore = score;
                }
            }

            return CreateMove(board, player, aiPlayer, maxCell.Value.Row, maxCell.Value.Col);
        }

        public int GetScoreForMove(char[,] board, char player, char enemy, Cell playCell, int score)
        {
            char[,] newBoard = (char[,]) board.Clone();
            newBoard[playCell.Row, playCell.Col] = player;

            _consoleView.ShowBoard(newBoard, player, enemy);

            char winner = GetWinner(newBoard);
            if (winner == player)
                return score + 100;
            else if (winner == enemy)
                return score -100;

            Cell? minCell = null;
            int minScore = 0;

            foreach (Cell cell in OpenCells(newBoard))
            {
                if (minCell == null)
                {
                    minCell = cell;
                    minScore = -1;
                }

                int cellScore = GetScoreForMove(newBoard, enemy, player, cell, score);
                if (cellScore < minScore)
                {
                    cellScore = score;
                    minCell = cell;
                }
            }

            return minScore;
        }

        public int GetScoreForMove5(char[,] board, Cell playCell, char enemy, int score)
        {
            char[,] newBoard = (char[,])board.Clone();
            newBoard[playCell.Row, playCell.Col] = playCell.Char;

           ShowBoard(newBoard);

            char winner = GetWinner(newBoard);
            if (winner != '\0')
            {
                if (winner == playCell.Char)
                    return score + 100;
                else
                    return score - 100;
            }

            // Cat game
            if (Flatten(board).All(ch => ch != '\0'))
                return 10;

            // Find minimum for opponent

            Cell? minCell = null;
            int maxMin =int.MinValue;

            foreach (Cell cell in OpenCells(newBoard))
            {
                if (minCell == null)
                {
                    minCell = cell;
                    maxMin = -1;
                }

                Cell newCell = new Cell {Char = enemy, Row = cell.Row, Col = cell.Col};
                int cellScore = -1 * GetScoreForMove5(newBoard, newCell, cell.Char, score);
                if (cellScore > maxMin)
                {
                    cellScore = score;
                    minCell = cell;
                }
            }

            return maxMin;
        }

    }
}
