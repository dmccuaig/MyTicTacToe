using System;

namespace TicTacToe.Engine
{
    public static class Move
    {
        public const int Opponent = -1;
        public const int None = default(int);   // same as zero
        public const int Player = 1;

        public static int ConvertTo<TFrom>(TFrom value, TFrom player, TFrom opponent)
        {
            if (value.Equals(player)) return Player;
            if (value.Equals(opponent)) return Opponent;
            return None;
        }

        public static TTo ConvertTo<TTo>(int value, TTo player, TTo opponent, TTo none)
        {
            switch (value)
            {
                case Opponent:
                    return opponent;
                case Player:
                    return player;
                case None:
                    return none;
                default:
                    throw new ApplicationException("Bad value");
            }
        }

    }
}