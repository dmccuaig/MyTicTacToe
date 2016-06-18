namespace TicTacToe.Tree
{
    /// <summary>
    /// A struct that represents a board game move.  The value field
    /// should only be manipulated by Minimax.
    /// </summary>
    public struct MinimaxMove
    {
        public int Row, Col;
        public int? Value;

        public MinimaxMove(int row, int col)
        {
            Row = row;
            Col = col;
            Value = null;
        }

        public MinimaxMove(int? value)
        {
            Row = Col = -1;
            Value = value;
        }

        public MinimaxMove(int row, int col, int? value)
        {
            Row = row;
            Col = col;
            Value = value;
        }
    }
}