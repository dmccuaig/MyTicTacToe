using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Engine;
using TicTacToe.UI;

namespace TicTacToe.Game
{
    public class XOGame
    {
        private readonly AiEngine _engine;
        private readonly View _view;

        public XOGame(AiEngine engine, View view)
        {
            _engine = engine;
            _view = view;
        }

        public void Play()
        {
            var board = new char[3, 3];
            char player;
            char aiPlayer;

            _view.PickPlayers(out player, out aiPlayer);
            ;

            if (_view.DoesComputerGoFirst())    //TODO: This isn't good....
            {
                board = _engine.GetMove(board, player, aiPlayer);
                _view.ShowBoard(board, player, aiPlayer);
            }
            else
            {
                _view.ShowBoard(board, player, aiPlayer);
            }

            char winner;
            do
            {
                RowCol rowCol = _view.GetPlayerMove(board, player);
                board[rowCol.Row, rowCol.Col] = player;
                board = _engine.GetMove(board, player, aiPlayer);
                winner = GetWinner(board, player, aiPlayer);
                _view.ShowBoard(board, player, aiPlayer, winner);
            } while (winner == View.None);
        }

        public char GetWinner(char[,] board, char player, char aiPlayer)
        {
            int emptyCount = board.Flatten().Count(ch => ch == View.None);
            int order = board.GetOrder();

            // All cells empty
            if (emptyCount == order * order)
                return View.None;

            char winner = GetWinnerByLine(board);
            if (winner != View.None) return winner;

            // Check for Cat game
            if (emptyCount == 0)
                return View.Cat;

            return View.None;
        }

        private static char GetWinnerByLine(char[,] board)
        {
            foreach (char player in GetWinnerForLine(board))
            {
                if (player != View.None) return player;
            }

            return View.None;
        }

        private static IEnumerable<char> GetWinnerForLine(char[,] board)
        {
            int order = board.GetOrder();

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
                if (cell != firstField) return View.None;
            }

            return firstField;
        }

    }
}
