using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Engine
{
    public class TicTacToeNode : INode
    {
        public int Player { get; }
        public int Opponent { get; }
        public INode[] Children { get; }
        public double Score { get; }
        public int Depth { get; set; }
    }
}
