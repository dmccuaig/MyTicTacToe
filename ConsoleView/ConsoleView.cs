using System;
using System.Text.RegularExpressions;

namespace TicTacToe.ConsoleUi
{
    public class ConsoleView // : View
    {
        private const char PlayerX = 'X';
        private const char PlayerO = 'O';
        private const char None = '\0';
        private const char Cat = 'C';

        readonly ConsoleReader _positionReader = new ConsoleReader("Move (1-9)", null, new Regex("[1-9]"));

        public void PickPlayers(out char human, out char ai)
        {
            string response = ConsoleReader.GetString("X or O? (X goes first)", "X", new Regex("[XxOo]"));
            human = response.ToUpper()[0];
            ai = human == 'X' ? 'O' : 'X';
        }

        public void ShowBoard(char[,] board, char human, char aiPlayer, char winner = None)
        {
            System.Console.WriteLine();
            int order = GetOrder(board);

            System.Console.Write("   ");
            for (int i = 0; i < order; i++)
                System.Console.Write(" {0} ", i);
            System.Console.WriteLine();

            ShowLine(order);

            int posn = 1;
            for (int i = 0; i <order; i++)
            {
                System.Console.Write(i + " |");
                for (int j = 0; j < order; j++)
                {
                    char cell = board[i, j];
                    if(cell == human)
                    {
                        System.Console.ForegroundColor = ConsoleColor.Yellow;
                        System.Console.Write(" " + cell + " ");
                    }
                    else if(cell == aiPlayer)
                    {
                        System.Console.ForegroundColor = ConsoleColor.Magenta;
                        System.Console.Write(" " + cell + " ");
                    }
                    else
                    {
                        System.Console.ForegroundColor = ConsoleColor.White;
                        System.Console.Write(" " + posn + " ");
                    }
                    System.Console.ResetColor();
                    posn++;
                }
                System.Console.Write("|");
                System.Console.WriteLine();
                ShowLine(order);
            }

            System.Console.WriteLine();

            if(winner == human)
                Console.WriteLine("You win!");
            else if(winner == aiPlayer)
                Console.WriteLine("Computer wins");
            else if(winner == Cat)
                Console.WriteLine("Cat Game");
        }

        private static void ShowLine(int order)
        {
            System.Console.Write("  +");
            System.Console.Write(new string('-', order * 3));
            System.Console.WriteLine('+');
        }

        public RowCol GetPlayerMove(char[,] board, char player)
        {
            int order = GetOrder(board);

            int row, col;
            for (;;)
            {
                string response = _positionReader.GetString();
                int position = int.Parse(response) - 1;
                row = position / order;
                col = position % order;
                if (row < order && col < order && board[row, col] == '\0')
                    break;

                System.Console.WriteLine("Invalid move");
            };

            return new RowCol(row,col);
        }

        private static int GetOrder(char[,] board)
        {
            if (board == null) return 0;
            return Math.Min(board.GetUpperBound(0), board.GetUpperBound(1)) + 1;
        }
    }
}