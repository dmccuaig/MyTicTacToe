using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Engine.AI
{
    public static class Search
    {
        public static INode BestMove(INode node, Func<INode,bool, IEnumerable<ScoredNode>> searchMethod )
        {
            bool maximizing = node.Player == Move.Player;
            var scoredNodes = searchMethod(node, maximizing);
            var bestScoredNode = maximizing ? scoredNodes.Max() : scoredNodes.Min();

            return bestScoredNode.Node;
        }
     
    }
}
