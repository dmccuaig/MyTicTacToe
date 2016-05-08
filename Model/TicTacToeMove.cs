namespace MyTicTacToe
{
    public sealed class TicTacToeMove
    {
        public TicTacToeMove(char[,] board, int moveRow = -1, int moveCol = -1, MoveState state = MoveState.Playing)
        {
            MoveRow = moveRow;
            MoveCol = moveCol;
            State = state;
            Board = (char[,])board.Clone();
        }

        public char[,] Board
        {
            get; private set;
        }

        public int MoveRow { get; private set; }
        public int MoveCol { get; private set; }
        public MoveState State { get; private set; }
    }

}
