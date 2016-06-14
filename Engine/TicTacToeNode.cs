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
            TriBoard = parent.TriBoard.CloneArray();
            _order = parent._order;
            _rootPlayer = parent._rootPlayer;
            _depth = parent._depth + 1;
            _player = -parent._player;
            _rowCounts = (int[])parent._rowCounts.Clone();
            _colCounts = (int[])parent._colCounts.Clone();
            _diagCounts = parent._diagCounts;
            _backDiagCounts = parent._backDiagCounts;

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
                    _rowCounts[row] += TriBoard[row, col];
                    _colCounts[col] += TriBoard[row, col];

                    _playedCells += Math.Abs(TriBoard[row, col]);
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
            _rowCounts[row] += TriBoard[row, col];
            _colCounts[col] += TriBoard[row, col];

            if (row == col)
                _diagCounts += TriBoard[row, col];

            if (row + col == _order - 1)
                _backDiagCounts += TriBoard[row, col];
        }

        public bool GetTerminalState()
        {
            // Can't be terminal until enough cells played
            if (_playedCells < _order) return false;

            //(i + (i >> 31)) ^ (i >> 31) <--> Math.Abs
            int cmpValue = _diagCounts;
            if (((cmpValue + (cmpValue >> 31)) ^ (cmpValue >> 31)) == _order)
            {
                _winner = _player;
                return true;
            }

            cmpValue = _backDiagCounts;
            if (((cmpValue + (cmpValue >> 31)) ^ (cmpValue >> 31)) == _order)
            {
                _winner = _player;
                return true;
            }

            for (int i = 0; i < _order; i++)
            {
                cmpValue = _rowCounts[i];
                if (((cmpValue + (cmpValue >> 31)) ^ (cmpValue >> 31)) == _order)
                {
                    _winner = _player;
                    return true;
                }

                cmpValue = _colCounts[i];
                if (((cmpValue + (cmpValue >> 31)) ^ (cmpValue >> 31)) == _order)
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

            //(i + (i >> 31)) ^ (i >> 31) <--> Math.Abs
            int cmpValue = _diagCounts;
            if (((cmpValue + (cmpValue >> 31)) ^ (cmpValue >> 31)) == _order)
            {
                _winner = _player;
                return true;
            }

            cmpValue = _backDiagCounts;
            if (((cmpValue + (cmpValue >> 31)) ^ (cmpValue >> 31)) == _order)
            {
                _winner = _player;
                return true;
            }

            cmpValue = _rowCounts[row];
            if (((cmpValue + (cmpValue >> 31)) ^ (cmpValue >> 31)) == _order)
            {
                _winner = _player;
                return true;
            }

            cmpValue = _colCounts[col];
            if (((cmpValue + (cmpValue >> 31)) ^ (cmpValue >> 31)) == _order)
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
