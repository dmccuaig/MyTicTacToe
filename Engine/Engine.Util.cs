using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Engine
{
    partial class AiEngine
    {
        public static IEnumerable<char> Flatten(char[,] board)
        {
            int order = GetOrder(board);

            for (int i = 0; i < order; i++)
                for (int j = 0; j < order; j++)
                    yield return board[i, j];
        }

        public static IEnumerable<Cell> Iterate(char[,] board)
        {
            int order = GetOrder(board);

            for (int i = 0; i < order; i++)
                for (int j = 0; j < order; j++)
                    yield return new Cell { Row = i, Col = j, Char = board[i, j] };
        }

        public static IEnumerable<Cell> OpenCells(char[,] board)
        {
            return Iterate(board).Where(cell => cell.Char == '\0');
        }

        public static int GetOrder(char[,] board)
        {
            if (board == null) return 0;
            return Math.Min(board.GetUpperBound(0), board.GetUpperBound(1)) + 1;
        }

        public struct Cell
        {
            public Cell(int row, int col, char ch)
            {
                Row = row;
                Col = col;
                Char = ch;
            }

            public int Row;
            public int Col;
            public char Char;

            public override string ToString()
            {
                return $"({Row},{Col}={Char}";
            }
        }

        public void ShowBoard(char[,] board)
        {
            Console.WriteLine();
            int order = GetOrder(board);

            Console.Write("   ");
            for (int i = 0; i < order; i++)
                Console.Write(" {0} ", i);
            Console.WriteLine();

            ShowLine(order);

            for (int i = 0; i < order; i++)
            {
                Console.Write(i + " |");
                for (int j = 0; j < order; j++)
                {
                    char cell = board[i, j];
                    if(cell == '\0')
                        Console.Write("   ");
                    else
                        Console.Write(" " + cell + " ");
                }
                Console.Write("|");
                Console.WriteLine();
                ShowLine(order);
            }

            Console.WriteLine();
        }

        private static void ShowLine(int order)
        {
            Console.Write("  +");
            Console.Write(new string('-', order * 3));
            Console.WriteLine('+');
        }

    }
}
