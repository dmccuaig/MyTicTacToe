using System;

namespace TicTacToe.Engine.AI
{
    public struct ScoredNode : IComparable<ScoredNode>
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
}