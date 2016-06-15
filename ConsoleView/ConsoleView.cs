using System;
using System.Text.RegularExpressions;
using TicTacToe.UI;

namespace TicTacToe.ConsoleUI
{
    public class ConsoleView : View
    {
        readonly ConsoleReader _positionReader = new ConsoleReader("Move: ", null, new Regex("[1-9]+"));

        public override void PickPlayers(out char human, out char ai)
        {
            string response = ConsoleReader.GetString("X or O?", "X", new Regex("[XxOo]"));
            human = response.ToUpper()[0];
            ai = human == 'X' ? 'O' : 'X';
        }

        public override bool DoesComputerGoFirst()
        {
            string response = ConsoleReader.GetString("Does the computer go first?", "N", new Regex("[YyNn]"));
            return response.ToUpper()[0] == 'Y';
        }

        public override void ShowBoard(char[,] board, char human, char aiPlayer, char winner = None)
        {
            Console.WriteLine();
            int order = GetOrder(board);

           Console.Write("   ");
            for (int i = 0; i < order; i++)
               Console.Write("  {0} ", i);
           Console.WriteLine();

            ShowLine(order);

            int posn = 1;
            for (int i = 0; i <order; i++)
            {
               Console.Write(i + " |");
                for (int j = 0; j < order; j++)
                {
                    char cell = board[i, j];
                    if(cell == human)
                    {
                       Console.ForegroundColor = ConsoleColor.Yellow;
                       Console.Write("  " + cell + " ");
                    }
                    else if(cell == aiPlayer)
                    {
                       Console.ForegroundColor = ConsoleColor.Magenta;
                       Console.Write("  " + cell + " ");
                    }
                    else
                    {
                       Console.ForegroundColor = ConsoleColor.White;
                       Console.Write(" {0,2} ", posn);
                    }
                   Console.ResetColor();
                    posn++;
                }
               Console.Write("|");
               Console.WriteLine();
                ShowLine(order);
            }

           Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            if (winner == human)
                Console.WriteLine("You win!");
            else if(winner == aiPlayer)
                Console.WriteLine("Computer wins");
            else if(winner == Cat)
                Console.WriteLine("Cat Game");
            Console.ResetColor();
        }

        private static void ShowLine(int order)
        {
           Console.Write("  +");
           Console.Write(new string('-', order * 4));
           Console.WriteLine('+');
        }

        public override RowCol GetPlayerMove(char[,] board, char player)
        {
            int order = GetOrder(board);

            int row, col;
            for (;;)
            {
                string response = _positionReader.GetString();
                int position = int.Parse(response) - 1;
                row = position / order;
                col = position % order;
                if (row < order && col < order && board[row, col] == None)
                    break;

               Console.WriteLine("Invalid move");
            }

            return new RowCol(row,col);
        }

        private static int GetOrder(char[,] board)
        {
            if (board == null) return 0;
            return Math.Min(board.GetUpperBound(0), board.GetUpperBound(1)) + 1;
        }
    }
}