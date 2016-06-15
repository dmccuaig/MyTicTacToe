using System;
using System.Collections.Generic;
using System.Text;

using TicTacToe.Tree;

namespace TicTacToe.Engine
{
    public class TicTacToeNode : Node
    {
        private const double WinValue = 1000;

        private readonly int _order;
        private readonly int _rootPlayer;
        private readonly int _depth;
        private readonly int _player;
        private int _winner;
        private int _playedCells;
        private readonly bool _isTerminal;

        private readonly int[] _rowCounts, _colCounts;
        private int _diagCounts;
        private int _backDiagCounts;

        public int[,] TriBoard { get; }

        public override bool IsTerminal => _isTerminal;

        public override IEnumerable<Node> Children
        {
            get
            {
                if (IsTerminal) yield break;

                for (int row = 0; row < _order; row++)
                {
                    for (int col = 0; col < _order; col++)
                    {
                        if (TriBoard[row, col] != Move.None) continue;
                        yield return new TicTacToeNode(this, row, col);
                    }
                }
            }
        }

        public TicTacToeNode(int rootPlayer, int[,] triBoard)
        {
            TriBoard = triBoard;
            _order = triBoard.GetOrder();
            _rootPlayer = rootPlayer;
            _depth = 0;
            _player = -rootPlayer;
            _rowCounts = new int[_order];
            _colCounts = new int[_order];

            Tally();
            _isTerminal = GetTerminalState();
            Score = _isTerminal ? _rootPlayer * _winner * WinValue : 0;
            //ToDebug();
        }

        public TicTacToeNode(TicTacToeNode parent, int row, int col)
        {
            _order = parent._order;
            _rootPlayer = parent._rootPlayer;
            _depth = parent._depth + 1;
            _player = -parent._player;

            _rowCounts = new int[_order];
            _colCounts = new int[_order];
            for (int i = 0; i < _order; i++)
            {
                _rowCounts[i] = parent._rowCounts[i];
                _colCounts[i] = parent._colCounts[i];
            }
            _diagCounts = parent._diagCounts;
            _backDiagCounts = parent._backDiagCounts;

            TriBoard = new int[_order, _order];
            for (int i = 0; i < _order; i++)
                for (int j = 0; j < _order; j++)
                    TriBoard[i, j] = parent.TriBoard[i, j];
            TriBoard[row, col] = _player;

            _playedCells = parent._playedCells + 1;

            ChildTally(row, col);
            _isTerminal = GetChildTerminalState(row, col);
            Score = _isTerminal ? _rootPlayer * _winner * WinValue : 0;
           // ToDebug();
        }

        public void Tally()
        {
            // Tally up rows and columns
            for (int row = 0; row < _order; row++)
            {
                for (int col = 0; col < _order; col++)
                {
                    int cellValue = TriBoard[row, col];
                    _rowCounts[row] += cellValue;
                    _colCounts[col] += cellValue;

                    if (cellValue != Move.None)
                        _playedCells++;
                }
            }

            // Tally up diagonal
            for (int rc = 0; rc < _order; rc++)
            {
                _diagCounts += TriBoard[rc, rc];
            }

            // Tally up reverse dialogal
            for (int r = 0, c = _order - 1; r < _order; r++, c--)
            {
                _backDiagCounts += TriBoard[r, c];
            }
        }

        public void ChildTally(int row, int col)
        {
            int cell = TriBoard[row, col];
            _rowCounts[row] += cell;
            _colCounts[col] += cell;

            if (row == col)
                _diagCounts += cell;

            if (row + col == _order - 1)
                _backDiagCounts += cell;
        }

        public bool GetTerminalState()
        {
            // Can't be terminal until enough cells played
            if (_playedCells < _order) return false;

            if (_diagCounts == _order || _diagCounts == -_order)
            {
                _winner = _player;
                return true;
            }

            if (_backDiagCounts == _order || _backDiagCounts == -_order)
            {
                _winner = _player;
                return true;
            }

            for (int i = 0; i < _order; i++)
            {
                int rowCount = _rowCounts[i];
                if (rowCount == _order || rowCount == -_order)
                {
                    _winner = _player;
                    return true;
                }

                int colCount = _colCounts[i];
                if (colCount == _order || colCount == -_order)
                {
                    _winner = _player;
                    return true;
                }
            }

            // Cat game
            if (_playedCells == _order * _order) return true;

            return false;
        }

        public bool GetChildTerminalState(int row, int col)
        {
            // Can't be terminal until enough cells played
            if (_playedCells < _order) return false;

            if (_diagCounts == _order || _diagCounts == -_order)
            {
                _winner = _player;
                return true;
            }

            if (_backDiagCounts == _order || _backDiagCounts == -_order)
            {
                _winner = _player;
                return true;
            }

            int rowCount = _rowCounts[row];
            if (rowCount == _order || rowCount == -_order)
            {
                _winner = _player;
                return true;
            }

            int colCount = _colCounts[col];
            if (colCount == _order || colCount == -_order)
            {
                _winner = _player;
                return true;
            }

            // Cat game
            if (_playedCells == _order * _order) return true;

            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _order; i++)
            {
                sb.Append("{");
                for (int j = 0; j < _order; j++)
                {
                    int cell = TriBoard[i, j];
                    sb.Append(" " + ToFriendly(cell));
                }
                sb.Append("}\n");
            }
            sb.Append($" Player: {ToFriendly(_rootPlayer)} M: {ToFriendly(_player)} W: {ToFriendly(_winner)} Depth: {_depth}, Score: {Score}");
            return sb.ToString();
        }

        private char ToFriendly(int mover)
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

        //private void ToDebug()
        //{
        //    Debug.WriteLine("");
        //    Debug.WriteLine(this);
        //    Debug.WriteLine("IsTerminal="+_isTerminal);
        //    Debug.WriteLine("DiagCount="+_diagCounts+" BackDiagCounts="+_backDiagCounts);
        //    Debug.Write("RowCounts=");
        //    for(int i = 0; i < _order; i++) Debug.Write(_rowCounts[i]+",");
        //    Debug.WriteLine("");
        //    Debug.Write("ColCounts=");
        //    for (int i = 0; i < _order; i++) Debug.Write(_colCounts[i] + ",");
        //    Debug.WriteLine("");

        //}

    }
}
