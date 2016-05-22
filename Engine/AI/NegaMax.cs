using System;
using System.Linq;

namespace TicTacToe.Engine.AI
{
    // Untested
    public class NegaMax
    {

/*
01 function negamax(node, depth, color)
02     if depth = 0 or node is a terminal node
03         return color * the heuristic value of node

04     bestValue := −∞
05     foreach child of node
06         v := −negamax(child, depth − 1, −color)
07         bestValue := max( bestValue, v )
08     return bestValue

Initial call for Player A's root node
rootNegamaxValue := negamax( rootNode, depth, 1)
rootMinimaxValue := rootNegamaxValue
Initial call for Player B's root node
rootNegamaxValue := negamax( rootNode, depth, −1)
rootMinimaxValue := −rootNegamaxValue 
*/

        public static double Score(INode node, int player)
        {
            if (node.Children.Any() == false)
                return player * node.Score;

            double bestValue = double.NegativeInfinity;
            foreach (var child in node.Children)
            {
                double v = Score(child, -player);
                bestValue = Math.Max(bestValue, v);
            }

            return bestValue;
        }

/*
01 function negamax(node, depth, α, β, color)
02     if depth = 0 or node is a terminal node
03         return color * the heuristic value of node

04     childNodes := GenerateMoves(node)
05     childNodes := OrderMoves(childNodes)
06     bestValue := −∞
07     foreach child in childNodes
08         v := −negamax(child, depth − 1, −β, −α, −color)
09         bestValue := max( bestValue, v )
10         α := max( α, v )
11         if α ≥ β
12             return β
13     return bestValue
Initial call for Player A's root node
rootNegamaxValue := negamax( rootNode, depth, −∞, +∞, 1)
*/

        public static double PrunedScore(INode node, double alpha, double beta, int player)
        {
            if (node.Children.Any() == false)
                return player * node.Score;

            double bestValue = double.NegativeInfinity;

            foreach (var child in node.Children)
            {
                double v = PrunedScore(child, -alpha, -beta, -player);
                bestValue = Math.Max(bestValue, v);
                alpha = Math.Max(alpha, v);
                if (alpha >= beta) return beta;
            }

            return bestValue;
        }

    }
}
