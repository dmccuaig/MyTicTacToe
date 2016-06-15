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
            TicTacToeNode bestMove = triBoard.GetOrder() <= 3
                ? (TicTacToeNode) Search.AlphaBeta(node)
                : (TicTacToeNode) Search.ParallelAlphaBeta(node);
            return bestMove.TriBoard;
        }

    }
}
