using System;
using TicTacToe.ConsoleUi;
using TicTacToe.Engine.AI;

namespace TicTacToe.Engine
{
    public partial class AiEngine
    {
        private ConsoleView _consoleView = new ConsoleView();

        public char[,] GetMove(char[,] board, char player = 'P', char opponent = 'O', char empty = '\0')
        {
            if (board == null) throw new ArgumentNullException(nameof(board));

            sbyte[,] triBoard = board.Convert2D(ch => Move.ConvertTo(ch,player,opponent));
            triBoard = GetBestMove(triBoard);
            return triBoard.Convert2D(tri => Move.ConvertTo(tri, player, opponent, empty));
        }

        public sbyte[,] GetBestMove(sbyte[,] triBoard)
        {
            var node = new TicTacToeNode(Move.Opponent, triBoard, Move.Opponent);
            //var bestMove = (TicTacToeNode)AlphaBeta.BestMove(node);
            var bestMove = (TicTacToeNode)Search.MiniMax(node);
            return bestMove.TriBoard;
        }

    }
}
