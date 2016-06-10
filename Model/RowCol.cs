namespace TicTacToe.Model
{
    public class RowCol
    {
        public static RowCol None = new RowCol();

        public RowCol() : this(-1, -1) { }

        public RowCol(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public int Row { get; }
        public int Col { get; }
    }
}
