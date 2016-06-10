﻿using System;
using System.Collections.Generic;

namespace TicTacToe.Game
{
    public static class ArrayExtensions
    {
        public static IEnumerable<T> Flatten<T>(this T[,] board, Func<T, bool> where = null)
        {
            int order = GetOrder(board);

            for (int i = 0; i < order; i++)
                for (int j = 0; j < order; j++)
                {
                    T el = board[i, j];
                    bool include = where != null ? where(el) : true;
                    if (include) yield return el;
                }
        }

        public static int GetOrder<T>(this T[,] board)
        {
            if (board == null) return 0;
            return Math.Min(board.GetUpperBound(0), board.GetUpperBound(1)) + 1;
        }

        public static T[,] CloneArray<T>(this T[,] source)
        {
            return (T[,]) source.Clone();
        }

    }
}
