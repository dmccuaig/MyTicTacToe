using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Tree.ParallelMinimax
{
    public class TicTacToeMinimax : Minimax
    {
        public override int MaxDepth { get; }
        public override TimeSpan TimeLimit { get; }
        public override int DegreeOfParallelism { get; }
        protected override bool TerminalTest(MinimaxSpot[,] state)
        {
            throw new NotImplementedException();
        }

        protected override int EvaluateHeuristic(MinimaxSpot[,] state)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<MinimaxMove> GetValidMoves(MinimaxSpot[,] state, bool isLightPlayer)
        {
            throw new NotImplementedException();
        }

        protected override MinimaxSpot[,] GetInsight(MinimaxSpot[,] state, MinimaxMove move, bool isLightPlayer)
        {
            throw new NotImplementedException();
        }
    }
}
