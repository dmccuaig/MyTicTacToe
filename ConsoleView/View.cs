namespace TicTacToe.ConsoleUi
{
    public abstract class View
    {
        public abstract void PickPlayers(out char human, out char ai);

       // public abstract void ShowBoard(char[,] board, char human, char aiPlayer, MoveState moveState = MoveState.Playing, RowCol playerMove = null, RowCol aiMove = null);
        public abstract RowCol GetPlayerMove(char[,] board, char player);
        //public abstract void ShowState(TicTacToeMove aiMove);
    }
}
