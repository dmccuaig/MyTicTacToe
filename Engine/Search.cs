using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Engine.AI
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

        /*
        *
         function NegaScout(node, depth, α, β, color)
   if node is a terminal node or depth = 0
       return color × the heuristic value of node
   for each child of node
       if child is not first child
           score := -pvs(child, depth-1, -α-1, -α, -color)       (* search with a null window *)
           if α < score < β                                      (* if it failed high,
               score := -pvs(child, depth-1, -β, -score, -color)        do a full re-search *)
       else
           score := -pvs(child, depth-1, -β, -α, -color)
       α := max(α, score)
       if α ≥ β
           break                                            (* beta cut-off *)
   return α
   */

    }
}
