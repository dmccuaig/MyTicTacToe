using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Engine
{
    public interface INode
    {
        int Player { get; }
        int Opponent { get; }
        INode[] Children { get; }
        double Score { get; }
        int Depth { get; set; }
    }

    struct ScoredNode : IComparable<ScoredNode>
    {
        public ScoredNode(INode node, double score)
        {
            Node = node;
            Score = score;
        }
        public INode Node;
        public double Score;

        public int CompareTo(ScoredNode other)
        {
            return Score.CompareTo(other.Score);
        }

        public override string ToString()
        {
            return $"{Node}Score: {Score}";
        }
    }

    public static class AlphaBeta
    {
        public const int None = 0;
        public const int Maximize = 1;
        public const int Minimize = 2;

        public static INode BestMove(INode node)
        {
            bool maximizing = node.Player == Maximize;

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
                        Search(child, double.NegativeInfinity, double.PositiveInfinity, !maximizing)
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
                    Search(child, double.NegativeInfinity, double.PositiveInfinity, !maximizing)
                )
            );

            Parallel.ForEach(node.Children, scoreChild);

            return scoredNodes;
        }

        private static double Search(INode node, double alpha, double beta, bool maximize)
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
