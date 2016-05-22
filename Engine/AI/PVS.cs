namespace TicTacToe.Engine
{
    public class Negascout
    {
        /*
         *
          function pvs(node, depth, α, β, color)
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
