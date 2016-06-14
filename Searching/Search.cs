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

            var scoredNodes = new List<Node>();

            foreach (var child in node.Children)
            {
                child.Score = AlphaBeta(child, double.NegativeInfinity, double.PositiveInfinity, !maximize);
                scoredNodes.Add(child);
            }

            Node bestNode = maximize ? scoredNodes.Max() : scoredNodes.Min();
            node.Score = bestNode.Score;

            return bestNode;
        }

        public static double AlphaBeta(Node node, double alpha, double beta, bool maximize)
        {
            if (node.IsTerminal) return node.Score;

            double score = maximize ? double.NegativeInfinity : double.PositiveInfinity;

            if (maximize)
            {
                foreach (var child in node.Children)
                {
                    score = Math.Max(score, AlphaBeta(child, alpha, beta, false));
                    alpha = Math.Max(alpha, score);
                    if (beta <= alpha) break;
                }
            }
            else
            {
                foreach (var child in node.Children)
                {
                    score = Math.Min(score, AlphaBeta(child, alpha, beta, true));
                    beta = Math.Min(beta, score);
                    if (beta <= alpha) break;
                }
            }

            return score;
        }

        public static Node ParallelMiniMax(Node node, bool maximize = true)
        {
            if (node.IsTerminal)
                return node;

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
    }
}
