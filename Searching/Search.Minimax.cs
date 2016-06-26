namespace TicTacToe.Tree
{
    partial class Search
    {
        public static Node Minimax(Node node, bool maximize = true)
        {
            if (node.IsTerminal) return node;

            return maximize
                ? MinimaxMax(node)
                : MinimaxMin(node);
        }

        public static Node MinimaxMin(Node node)
        {
            if (node.IsTerminal) return node;

            Node best = Node.MaximalNode;

            foreach (var child in node.Children)
            {
                MinimaxMax(child);
                if (child.Score < best.Score)
                    best = child;
            }

            node.SetScoreFromChild(best);
            return best;
        }

        public static Node MinimaxMax(Node node)
        {
            if (node.IsTerminal) return node;

            Node best = Node.MinimalNode;

            foreach (var child in node.Children)
            {
                MinimaxMin(child);
                if (child.Score > best.Score)
                    best = child;
            }

            node.SetScoreFromChild(best);
            return best;
        }

    }
}
