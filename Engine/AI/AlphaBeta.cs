using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Engine.AI
{
    public static class AlphaBeta
    {

        public static INode BestMove(INode node)
        {
            bool maximizing = node.Player == Move.Player;

            var scoredNodes = GetScoredNodes(node, maximizing);
            //var scoredNodes = ParallelGetScoredNodes(node, maximizing);
            var bestScoredNode = maximizing ? scoredNodes.Max() : scoredNodes.Min();

            return bestScoredNode.Node;
        }

        private static IEnumerable<ScoredNode> GetScoredNodes(INode node, bool maximizing)
        {
            var scoredNodes = new List<ScoredNode>();

            foreach (var child in node.Children)
            {
                scoredNodes.Add(
                    new ScoredNode(
                        child,
                        Score(child, double.NegativeInfinity, double.PositiveInfinity, !maximizing)
                    )
                );
            }

            return scoredNodes;
        }

        private static IEnumerable<ScoredNode> ParallelGetScoredNodes(INode node, bool maximizing)
        {
            var scoredNodes = new ConcurrentBag<ScoredNode>();

            Action<INode> scoreChild = child => scoredNodes.Add(
                new ScoredNode(
                    child,
                    Score(child, double.NegativeInfinity, double.PositiveInfinity, !maximizing)
                )
            );

            Parallel.ForEach(node.Children, scoreChild);

            return scoredNodes;
        }

        /*
01 function alphabeta(node, depth, α, β, maximizingPlayer)
02      if depth = 0 or node is a terminal node
03          return the heuristic value of node
04      if maximizingPlayer
05          v := -∞
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

        private static double Score(INode node, double alpha, double beta, bool maximize)
        {
            if (node.Children.Any() == false)
                return node.Score;

            double score = maximize ? double.NegativeInfinity : double.PositiveInfinity;

            if (maximize)
            {
                foreach (var child in node.Children)
                {
                    score = Math.Max(score, Score(child, alpha, beta, !maximize));
                    alpha = Math.Max(alpha, score);
                    if (beta <= alpha) break;
                }
            }
            else
            {
                foreach (var child in node.Children)
                {
                    score = Math.Min(score, Score(child, alpha, beta, !maximize));
                    beta = Math.Min(beta, score);
                    if (beta <= alpha) break;
                }
            }

            return score;
        }

    }
}
