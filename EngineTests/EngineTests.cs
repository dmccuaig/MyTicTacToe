using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using MyEngine;

namespace EngineTests
{
    [TestClass]
    public class EngineTests
    {
        const char _ = '\0';
        private const char X = 'X';
        private const char O = 'O';

        readonly Engine _engine = new Engine();
        private const char _player = 'X';
        private const char _aiPlayer = 'O';

        [TestMethod]
        public void IsAiWinTests()
        {
            char[,] b;
            const MoveState expectedState = MoveState.AiWin;

            b = new char[,]
            {
                {O,O,O},
                {_,O,_},
                {_,_,_},
            };
            Assert.AreEqual(_engine.GetMoveState(b, _player, _aiPlayer), expectedState);

            b = new char[,]
            {
                {O,_,_},
                {O,O,_},
                {O,_,_},
            };
            Assert.AreEqual(_engine.GetMoveState(b, _player, _aiPlayer), expectedState);

            b = new char[,]
            {
                {X,_,O},
                {X,O,_},
                {O,_,_},
            };
            Assert.AreEqual(_engine.GetMoveState(b, _player, _aiPlayer), expectedState);

            b = new char[,]
            {
                {O,_,_},
                {X,O,_},
                {X,_,O },
            };
            Assert.AreEqual(_engine.GetMoveState(b, _player, _aiPlayer), expectedState);
        }

        [TestMethod]
        public void IsPlayerWinTests()
        {
            char[,] b;
            MoveState expectedState = MoveState.PlayerWin;


            b = new char[,]
            {
                {X,X,X},
                {X,O,O},
                {O,O,X},
            };
            Assert.AreEqual(_engine.GetMoveState(b, _player, _aiPlayer), expectedState);
        }

        [TestMethod]
        public void IsNullNameTests()
        {
            char[,] b;
            MoveState expectedState = MoveState.NullGame;

            b = new char[,]
            {
                {_,_,_},
                {_,_,_},
                {_,_,_},
            };
            Assert.AreEqual(_engine.GetMoveState(b, _player, _aiPlayer), expectedState);
        }

        [TestMethod]
        public void IsPlayingTests()
        {
            char[,] b;
            MoveState expectedState = MoveState.Playing;

            b = new char[,]
            {
                {_,_,_},
                {_,X,_},
                {_,_,_},
            };
            Assert.AreEqual(_engine.GetMoveState(b, _player, _aiPlayer), expectedState);
            b = new char[,]
            {
                {_,_,_},
                {_,X,O},
                {_,_,_},
            };

            Assert.AreEqual(_engine.GetMoveState(b, _player, _aiPlayer), expectedState);
        }

        [TestMethod]
        public void IsCatGameTests()
        {
            char[,] b;
            MoveState expectedState = MoveState.CatGame;

            b = new char[,]
            {
                {X,O,X},
                {X,O,O},
                {O,X,X},
            };

            Assert.AreEqual(_engine.GetMoveState(b, _player, _aiPlayer), expectedState);
        }

    }
}
