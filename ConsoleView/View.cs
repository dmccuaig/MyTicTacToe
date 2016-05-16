using TicTacToe.Model;

namespace TicTacToe.ConsoleView
{
    public abstract class View
    {
        public abstract void PickPlayers(out char player, out char aiPlayer);
        public abstract void ShowBoard(char[,] board, char player, char aiPlayer, TicTacToeMove playerMove = null, TicTacToeMove aiMove = null);
        public abstract TicTacToeMove GetPlayerMove(char[,] board, char player);
        public abstract void ShowState(TicTacToeMove aiMove);
    }
}
