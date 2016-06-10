using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TicTacToe.Engine.AI;

namespace TicTacToe.Engine
{
    public class TicTacToeNode : Node
    {
        private const double WinValue = 1000;

        private readonly int _order;
        private readonly sbyte _player;
        private readonly int _depth;
        private readonly sbyte _toPlay;
        private sbyte _winner;
        private readonly Lazy<bool> _isTerminal;

        public sbyte[,] TriBoard { get; }

        public override bool IsTerminal => _isTerminal.Value;

        public override IEnumerable<Node> Children
        {
            get
            {
                if (IsTerminal)
                    yield break;

                for (int i = 0; i < _order; i++)
                {
                    for (int j = 0; j < _order; j++)
                    {
                        if (TriBoard[i, j] != Move.None) continue;

                        var childBoard = TriBoard.CloneArray();
                        childBoard[i, j] = _toPlay;
                        var childNode = new TicTacToeNode(_player, childBoard, Move.Negate(_toPlay), _depth + 1);
                        yield return childNode;
                    }
                }
            }
        }

        public TicTacToeNode(sbyte player, sbyte[,] triBoard, sbyte toPlay, int depth = 0)
        {
            TriBoard = triBoard;
            _order = triBoard.GetOrder();
            _player = player;
            _depth = depth;
            _toPlay = toPlay;
            _isTerminal = new Lazy<bool>(() => GetTerminalState());
        }

        private bool GetTerminalState()
        {
            int emptyCount = TriBoard.Flatten().Count(cell => cell == Move.None);
            if (emptyCount == _order * _order) return false; // Empty board

            _winner = GetWinnerForLine().FirstOrDefault(p => p != Move.None);
            Score = _winner == default(sbyte) ? 0 :  _player * _winner * WinValue;
            return _winner != Move.None || emptyCount == 0;  // Terminal if there is a winner or full board
        }

        private IEnumerable<sbyte> GetWinnerForLine()
        {
            for (int i = 0; i < _order; i++)
            {
                // Check row
                yield return FindWinner(i, 0, 0, 1);

                // Check row
                yield return FindWinner(0, i, 1, 0);
            }

            // Check diagonals - top left to bottom right
            yield return FindWinner(0, 0, 1, 1);

            // Check diagonals - bottom left to top right

            yield return FindWinner(_order - 1, 0, -1, 1);
        }

        private sbyte FindWinner(int startRow, int startCol, int dr, int dc)
        {
            sbyte firstField = TriBoard[startRow, startCol];
            for (int i = 0; i < _order; i++)
            {
                int r = startRow + dr * i;
                int c = startCol + dc * i;

                sbyte cell = TriBoard[r, c];
                if (cell != firstField) return Move.None;
            }

            return firstField;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ ");
            for (int i = 0; i < _order; i++)
            {
                sb.Append("{");
                for (int j = 0; j < _order; j++)
                {
                    sbyte cell = TriBoard[i, j];
                    sb.Append(" " + ToFriendly(cell));
                }
                sb.Append("}\n");
            }
            sb.Append(" }");
            sb.Append($" Player: {ToFriendly(_player)} M: {ToFriendly(_toPlay)} W: {ToFriendly(_winner)} Depth: {_depth}, Score: {Score}");
            return sb.ToString();
        }

        private char ToFriendly(sbyte mover)
        {
            switch (mover)
            {
                case Move.Player:
                    return 'O';
                case Move.Opponent:
                    return 'X';
                default:
                    return '_';
            }
        }

    }
}
