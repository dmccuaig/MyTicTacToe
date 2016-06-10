using System;
using System.Collections.Generic;

namespace TicTacToe.Engine.AI
{
    public abstract class Node : IComparable<Node>
    {
        public virtual double Score { get; set; }
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
    }
}
