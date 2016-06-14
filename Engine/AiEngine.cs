using System;
using TicTacToe.Tree;

namespace TicTacToe.Engine
{
    public class AiEngine
    {
        public char[,] GetMove(char[,] board, char player = 'P', char opponent = 'O', char empty = '\0')
        {
            if (board == null) throw new ArgumentNullException(nameof(board));

            int[,] triBoard = board.Convert2D(ch => Move.ConvertTo(ch, player, opponent));
            triBoard = GetBestMove(triBoard);
            return triBoard.Convert2D(tri => Move.ConvertTo(tri, player, opponent, empty));
        }

        public int[,] GetBestMove(int[,] triBoard)
        {
            Node node = new TicTacToeNode(Move.Opponent, triBoard);
            //var bestMove = (TicTacToeNode)Search.MiniMax(node);
            //var bestMove = (TicTacToeNode)Search.ParallelMiniMax(node);
            var bestMove = (TicTacToeNode)Search.AlphaBeta(node);
            //var bestMove = (TicTacToeNode) AlphaBeta.BestMove(node);
            return bestMove.TriBoard;
        }

    }
}
