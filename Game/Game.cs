using Model;
using MyEngine;
using MyTicTacToe;

namespace Game
{
    public class Game
    {
        private readonly Engine _engine;
        private readonly View _view;
        private char[,] _board = new char[3, 3];
        private char _player;
        private char _aiPlayer;

        public Game(Engine engine, View view)
        {
            _engine = engine;
            _view = view;
        }

        public void Play()
        {
            _view.PickPlayers(out _player, out _aiPlayer);

            TicTacToeMove aiMove;

            if (_aiPlayer == 'X')
            {
                aiMove = _engine.GetMove(_board, _player, _aiPlayer);
                _board = aiMove.Board;
                _view.ShowBoard(_board, _player, _aiPlayer, aiMove: aiMove);
            }
            else
            {
                _view.ShowBoard(_board, _player, _aiPlayer);
                aiMove = new TicTacToeMove(_board);
            }

            while (aiMove.State == MoveState.Playing)
            {
                TicTacToeMove playerMove = _view.GetPlayerMove(_board, _player);
                _board = playerMove.Board;

                aiMove = _engine.GetMove(_board, _player, _aiPlayer);
                _board = aiMove.Board;

                _view.ShowBoard(_board, _player, _aiPlayer, playerMove, aiMove);
            }

            _view.ShowState(aiMove);

        }

    }
}
