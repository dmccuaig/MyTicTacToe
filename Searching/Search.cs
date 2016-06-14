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
            if (node.IsTerminal)
                return node;

            var scoredNodes = new List<Node>();

            foreach (var child in node.Children)
            {
                child.Score = AlphaBeta(child, double.NegativeInfinity, double.PositiveInfinity, maximize);
                scoredNodes.Add(child);
            }

            Node bestNode = maximize ? scoredNodes.Max() : scoredNodes.Min();
            node.Score = bestNode.Score;

            return bestNode;
        }

        #region doesntwork
        //public static double AlphaBeta(Node node, double alpha, double beta, bool maximize)
        //{
        //    if (node.IsTerminal)
        //        return node.Score;

        //    double bestValue;
        //    if (maximize)
        //    {
        //        bestValue = alpha;
        //        foreach (var child in node.Children)
        //        {
        //            double childValue = AlphaBeta(child, bestValue, beta, false);
        //            bestValue = Math.Max(bestValue, childValue);
        //            if (beta <= bestValue) break;
        //        }
        //    }
        //    else
        //    {
        //        bestValue = beta;
        //        foreach (var child in node.Children)
        //        {
        //            double childValue = AlphaBeta(child, alpha, bestValue, true);
        //            bestValue = Math.Min(bestValue, childValue);
        //            if (bestValue <= alpha) break;
        //        }
        //    }

        //    return bestValue;
        //}
        #endregion

        public static double AlphaBeta(Node node, double alpha, double beta, bool maximize)
        {
            if (node.IsTerminal)
                return node.Score;

            if (maximize)
            {
                double v = double.NegativeInfinity;
                foreach (var child in node.Children)
                {
                    v = Math.Max(v, AlphaBeta(child, alpha, beta, false));
                    alpha = Math.Max(alpha, v);
                    if (beta <= alpha) break;
                }
                return v;
            }
            else
            {
                double v = double.PositiveInfinity;
                foreach (var child in node.Children)
                {
                    v = Math.Min(v, AlphaBeta(child, alpha, beta, true));
                    beta = Math.Min(beta, v);
                    if (beta <= alpha) break;
                }

                return v;
            }
        }

        /*
        01 function alphabeta(node, depth, α, β, maximizingPlayer)
        02      if depth = 0 or node is a terminal node
        03          return the heuristic value of node
        04      if maximizingPlayer
        05          v = -∞
        06          for each child of node
        07              v := max(v, alphabeta(child, depth - 1, α, β, FALSE))
        08              α := max(α, v)
        09              if β ≤ α
        10                  break (* β cut-off *)
        11          return v
        12      else
        13          v := ∞
        14          for each child of node
        15              v := min(v, alphabeta(child, depth - 1, α, β, TRUE))
        16              β := min(β, v)
        17              if β ≤ α
        18                  break (* α cut-off *)
        19          return v
        (* Initial call *)
        alphabeta(origin, depth, -∞, +∞, TRUE)
        */

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

    public static class AlphaBeta
    {
        public const int None = 0;
        public const int Maximize = 1;
        public const int Minimize = 2;

        public static Node BestMove(Node node)
        {
            if (node.IsTerminal)
                return node;

            bool maximizing = true;

            var scoredNodes = GetScoredNodes(node, maximizing);
            if (maximizing)
            {
                var t = new Tuple<Node, double>(null,double.NegativeInfinity);
                foreach (var tuple in scoredNodes)
                {
                    if (tuple.Item2 > t.Item2)
                        t = tuple;
                }
                return t.Item1;
            }
            else
            {
                var t = new Tuple<Node, double>(null, double.PositiveInfinity);
                foreach (var tuple in scoredNodes)
                {
                    if (tuple.Item2 < t.Item2)
                        t = tuple;
                }
                return t.Item1;
            }
        }

        private static IEnumerable<Tuple<Node, double>> GetScoredNodes(Node node, bool maximizing)
        {
            var scoredNodes = new List<Tuple<Node,double>>();

            foreach (var child in node.Children)
            {
                scoredNodes.Add(
                    Tuple.Create(
                        child,
                        Search(child, double.NegativeInfinity, double.PositiveInfinity, !maximizing)
                    )
                );
            }

            return scoredNodes;
        }

        private static double Search(Node node, double alpha, double beta, bool maximize)
        {
            if (node.Children.Any() == false)
                return node.Score;

            double score = maximize ? double.NegativeInfinity : double.PositiveInfinity;

            if (maximize)
            {
                foreach (var child in node.Children)
                {
                    score = Math.Max(score, Search(child, alpha, beta, !maximize));
                    alpha = Math.Max(alpha, score);
                    if (beta <= alpha) break;
                }
            }
            else
            {
                foreach (var child in node.Children)
                {
                    score = Math.Min(score, Search(child, alpha, beta, !maximize));
                    beta = Math.Min(beta, score);
                    if (beta <= alpha) break;
                }
            }

            return score;
        }


    }
}
