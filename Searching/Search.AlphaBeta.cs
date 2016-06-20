using System;

namespace TicTacToe.Tree
{
    public static partial class Search
    {
        public static Node AlphaBeta(Node node, bool maximize = true)
        {
            if (node.IsTerminal) return node;

            return maximize
                ? AlphaBetaMax(node)
                : AlphaBetaMin(node);
        }

        public static Node AlphaBetaMax(Node node, double alpha = double.NegativeInfinity, double beta = double.PositiveInfinity)
        {
            if (node.IsTerminal) return node;

            Node best = Node.MinimalNode;

            foreach (var child in node.Children)
            {
                AlphaBetaMin(child, alpha, beta);
                if (child.Score > best.Score)
                    best = child;

                alpha = Math.Max(alpha, best.Score);
                if (beta <= alpha) break;
            }

            node.Score = best.Score;
            return best;
        }

        public static Node AlphaBetaMin(Node node, double alpha = double.NegativeInfinity, double beta = double.PositiveInfinity)
        {
            if (node.IsTerminal) return node;

            Node best = Node.MaximalNode;

            foreach (var child in node.Children)
            {
                AlphaBetaMax(child, alpha, beta);
                if (child.Score < best.Score)
                    best = child;

                beta = Math.Min(beta, best.Score);
                if (beta <= alpha) break;
            }

            node.Score = best.Score;
            return best;
        }

    }
}
