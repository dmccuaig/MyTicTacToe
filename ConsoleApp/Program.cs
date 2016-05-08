using MyTicTacToe;
using System;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine engine = new Engine();
            View view = new ConsoleView();

            Game game = new Game(engine, view);

            game.Play();

            Console.Write("Press a key to continue...");
            Console.ReadKey();
        }
    }
}
