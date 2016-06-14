namespace TicTacToe.UI
{
    public abstract class View
    {
        public const char PlayerX = 'X';
        public const char PlayerO = 'O';
        public const char None = '\0';
        public const char Cat = 'C';

        public abstract void PickPlayers(out char human, out char ai);
        public abstract void ShowBoard(char[,] board, char human, char aiPlayer, char winner = None);
        public abstract RowCol GetPlayerMove(char[,] board, char player);
        public abstract bool DoesComputerGoFirst();
    }
}
