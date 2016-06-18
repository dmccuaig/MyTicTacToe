using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Tree
{
    public abstract class Node : IComparable<Node>
    {
        public static Node MinimalNode = new ExtremeScoreNode(double.NegativeInfinity);
        public static Node MaximalNode = new ExtremeScoreNode(double.PositiveInfinity);

        public double Score { get; set; }
        public abstract IEnumerable<Node> Children { get; }
        public abstract bool IsTerminal { get; }

        public int CompareTo(Node other)
        {
            return Score.CompareTo(other.Score);
        }

        public override string ToString()
        {
            return $"{this} Score: {Score}";
        }

        private class ExtremeScoreNode : Node
        {
            public ExtremeScoreNode(double score)
            {
                Score = score;
            }

            public override IEnumerable<Node> Children => Enumerable.Empty<Node>();
            public override bool IsTerminal => true;
        }

    }
}
