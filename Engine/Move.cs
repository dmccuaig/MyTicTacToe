using System;

namespace TicTacToe.Engine.AI
{
    public static class Move
    {
        public const sbyte Opponent = -1;
        public const sbyte None = default(sbyte);   // same as zero
        public const sbyte Player = 1;

        public static sbyte ConvertTo<TFrom>(TFrom value, TFrom player, TFrom opponent)
        {
            if (value.Equals(player)) return Move.Player;
            if (value.Equals(opponent)) return Move.Opponent;
            return Move.None;
        }

        public static TTo ConvertTo<TTo>(sbyte value, TTo player, TTo opponent, TTo none)
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

        public static sbyte Multiply(sbyte a, sbyte b)
        {
            return (sbyte) (a*b);
        }

        public static sbyte Negate(sbyte a)
        {
            return (sbyte) -a;
        }

    }
}