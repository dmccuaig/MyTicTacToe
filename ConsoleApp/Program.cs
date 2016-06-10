using System;
using TicTacToe.ConsoleUi;
using TicTacToe.Engine;
using TicTacToe.Game;

namespace TicTacToe.ConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            AiEngine engine = new AiEngine();
            ConsoleView view = new ConsoleView();

            XOGame game = new XOGame(engine, view);

            game.Play();

            Console.Write("Press a key to continue...");
            Console.ReadKey();
        }
    }
}
