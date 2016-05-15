using System;
using MyEngine;
using MyTicTacToe;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            Engine engine = new Engine();
            View view = new ConsoleView.ConsoleView();

            Game.Game game = new Game.Game(engine, view);

            game.Play();

            Console.Write("Press a key to continue...");
            Console.ReadKey();
        }
    }
}
