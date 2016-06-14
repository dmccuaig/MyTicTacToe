using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Tree
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

        public static Node AlphaBeta(Node node, bool maximize = true)
        {
            return AlphaBeta(node, double.NegativeInfinity, double.PositiveInfinity, maximize);
        }

        public static Node AlphaBeta(Node node, double alpha, double beta, bool maximize)
        {
            if (node.IsTerminal)
                return node;

            double score = maximize ? double.NegativeInfinity : double.PositiveInfinity;

            Node n = node;
            if (maximize)
            {
                foreach (var child in node.Children)
                {
                    AlphaBeta(child, alpha, beta, !maximize);
                    if (child.Score > score)
                    {
                        score = child.Score;
                        n = child;
                    }
                    alpha = Math.Max(alpha, score);
                    if (beta <= alpha) break;
                }
            }
            else
            {
                foreach (var child in node.Children)
                {
                    AlphaBeta(child, alpha, beta, !maximize);
                    if (child.Score < score)
                    {
                        score = child.Score;
                        n = child;
                    }
                    beta = Math.Min(beta, score);
                    if (beta <= alpha) break;
                }
            }

            n.Score = score;
            return n;
        }

        /*
        01 function alphabeta(node, depth, α, β, maximizingPlayer)
        02      if depth = 0 or node is a terminal node
        03          return the heuristic value of node
        04      if maximizingPlayer
        05          v = -∞
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

        public static Node ParallelMiniMax(Node node, bool maximize = true)
        {
            if (node.IsTerminal)
                return node;

            var scoredNodes = new ConcurrentBag<Node>();
            Action<Node> processNodes = child =>
            {
                MiniMax(child, !maximize);
                scoredNodes.Add(child);
            };

            Parallel.ForEach(node.Children, processNodes);

            Node bestNode = maximize ? scoredNodes.Max() : scoredNodes.Min();
            node.Score = bestNode.Score;

            return bestNode;
        }


    }

    public static class AlphaBeta
    {
        public const int None = 0;
        public const int Maximize = 1;
        public const int Minimize = 2;

        public static Node BestMove(Node node)
        {
            bool maximizing = true;
            //bool maximizing = node.Player == Maximize;

            var scoredNodes = GetScoredNodes(node, maximizing);
            //var scoredNodes = ParallelGetScoredNodes(node, maximizing);
            var bestScoredNode = maximizing ? scoredNodes.Max() : scoredNodes.Min();

            return bestScoredNode;
        }

        private static IEnumerable<Node> GetScoredNodes(Node node, bool maximizing)
        {
            var scoredNodes = new List<Node>();

            foreach (var child in node.Children)
            {
                //scoredNodes.Add(
                //    new Node(
                //        child,
                //        Search(child, double.NegativeInfinity, double.PositiveInfinity, !maximizing)
                //    )
                //);
            }

            return scoredNodes;
        }

        private static double Search(Node node, double alpha, double beta, bool maximize)
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
