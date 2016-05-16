using System;
using System.Text.RegularExpressions;
using TicTacToe.Model;

namespace TicTacToe.ConsoleView
{
    public class ConsoleView : View
    {
        readonly ConsoleReader _positionReader = new ConsoleReader("Move (1-9)", null, new Regex("[1-9]"));

        public override void PickPlayers(out char player, out char aiPlayer)
        {
            string response = ConsoleReader.GetString("X or O? (X goes first)", "X", new Regex("[XxOo]"));
            player = response.ToUpper()[0];
            aiPlayer = player == 'X' ? 'O' : 'X';
        }

        public override void ShowBoard(char[,] board, char player, char aiPlayer, TicTacToeMove playerMove = null, TicTacToeMove aiMove = null)
        {
            var playerColor = ConsoleColor.Yellow;
            var aiColor = ConsoleColor.Magenta;

            Console.WriteLine();
            ShowMove("Player", player, playerColor, playerMove);
            int order = GetOrder(board);

            Console.Write("   ");
            for (int i = 0; i < order; i++)
                Console.Write(" {0} ", i);
            Console.WriteLine();

            ShowLine(order);
 
            int posn = 1;
            for (int i = 0; i <order; i++)
            {
                Console.Write(i + " |");
                for (int j = 0; j < order; j++)
                {
                    char cell = board[i, j];
                    if(cell == player)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" " + cell + " ");
                    }
                    else if(cell == aiPlayer)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(" " + cell + " ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" " + posn + " ");
                    }
                    Console.ResetColor();
                    posn++;
                }
                Console.Write("|");
                Console.WriteLine();
                ShowLine(order);
            }

            ShowMove("AI", aiPlayer, aiColor, aiMove);
            Console.WriteLine();
        }

        private static void ShowLine(int order)
        {
            Console.Write("  +");
            Console.Write(new string('-', order * 3));
            Console.WriteLine('+');
        }

        public override void ShowState(TicTacToeMove aiMove)
        {
            if (aiMove == null) return;

            switch (aiMove.State)
            {
                case MoveState.CatGame:
                    Console.WriteLine("No Winner");
                    break;
                case MoveState.AiWin:
                    Console.WriteLine("AI Wins");
                    break;
                case MoveState.PlayerWin:
                    Console.WriteLine("Player Wins");
                    break;
            }
        }

        public override TicTacToeMove GetPlayerMove(char[,] board, char player)
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

                Console.WriteLine("Invalid move");
            };

            return new TicTacToeMove(board, player: player, moveRow: row, moveCol: col);
        }

        private void ShowMove(string who, char player, ConsoleColor color, TicTacToeMove move)
        {
            if (move == null) return;
            if (move.MoveRow == -1 || move.MoveCol == -1) return;

            Console.Write("\n{0} ", who);
            Console.Write('[');
            Console.ForegroundColor = color;
            Console.Write(player);
            Console.ResetColor();
            Console.Write(']');

            Console.WriteLine(" moves ({0},{1})\n", move.MoveRow, move.MoveCol);
        }

        private static int GetOrder(char[,] board)
        {
            if (board == null) return 0;
            return Math.Min(board.GetUpperBound(0), board.GetUpperBound(1)) + 1;
        }
    }
}