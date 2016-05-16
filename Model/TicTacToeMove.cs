namespace TicTacToe.Model
{
    public sealed class TicTacToeMove
    {
        public TicTacToeMove(char[,] board, char player = '\0', int moveRow = -1, int moveCol = -1, MoveState state = MoveState.Playing)
        {
            MoveRow = moveRow;
            MoveCol = moveCol;
            State = state;
            Board = (char[,])board.Clone();
            if(moveRow != -1 && moveCol != -1 && player != '\0')
            {
                Board[MoveRow, MoveCol] = player;
            }
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
