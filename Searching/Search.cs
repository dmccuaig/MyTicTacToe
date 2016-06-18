using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Tree
{
    public static class Search
    {
        public static Node MiniMax(Node node, bool maximize = true)
        {
            if (node.IsTerminal)
                return node;

            var scoredNodes = new List<Node>();

            foreach (var child in node.Children)
            {
                MiniMax(child, !maximize);
                scoredNodes.Add(child);
            }

            Node bestNode = maximize ? scoredNodes.Max() : scoredNodes.Min();
            node.Score = bestNode.Score;

            return bestNode;
        }

        public static Node AlphaBeta(Node node, bool maximize = true)
        {
            if (node.IsTerminal) return node;

            return maximize
                ? AlphaBetaMax(node)
                : AlphaBetaMin(node);
        }

        public static Node ParallelAlphaBeta(Node node, bool maximize = true)
        {
            if (node.IsTerminal) return node;

            return maximize
                ? ParallelAlphaBetaMax(node)
                : ParallelAlphaBetaMin(node);
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

        public static Node ParallelMiniMax(Node node, bool maximize = true)
        {
            if (node.IsTerminal) return node;

            var scoredNodes = new ConcurrentBag<Node>();
            Action<Node> processNodes = child =>
            {
                MiniMax(child, !maximize);
                scoredNodes.Add(child);
            };

            Parallel.ForEach(node.Children, processNodes);

            Node bestNode = maximize ? scoredNodes.Max() : scoredNodes.Min();
            node.Score = bestNode.Score;

            return bestNode;
        }

        public static Node ParallelAlphaBetaMax(Node node, double alpha = double.NegativeInfinity, double beta = double.PositiveInfinity)
        {
            Node best = Node.MinimalNode;
            object lockObject = new object();
            double score = best.Score;

            Action<Node,ParallelLoopState> processChildren = (child,state) =>
            {
                AlphaBetaMin(child, alpha, beta);
                    lock (lockObject)
                    {
                        if (child.Score > score)
                        {
                            best = child;
                            score = best.Score;
                        }
                }

                alpha = Math.Max(alpha, score);
                if (beta <= alpha) state.Break();
            };

            Parallel.ForEach(node.Children, processChildren);

            node.Score = best.Score;
            return best;
        }

        public static Node ParallelAlphaBetaMin(Node node, double alpha = double.NegativeInfinity, double beta = double.PositiveInfinity)
        {
            Node best = Node.MaximalNode;
            object lockObject = new object();
            double score = best.Score;

            Action<Node, ParallelLoopState> processChildren = (child, state) =>
            {
                AlphaBetaMax(child, alpha, beta);
                lock (lockObject)
                {
                    if (child.Score < score)
                    {
                        best = child;
                        score = best.Score;
                    }
                }

                beta = Math.Min(beta, score);
                if (beta <= alpha) state.Break();
            };

            Parallel.ForEach(node.Children, processChildren);

            node.Score = best.Score;
            return best;
        }

    }
}
