using NUnit.Framework;
using TicTacToe.Engine;

namespace Engine.Test
{
    [TestFixture]
    public class EngineTests
    {
        const char _ = '\0';
        private const char X = 'X';
        private const char O = 'O';

        readonly AiEngine _engine = new AiEngine();
        private const char _player = 'X';
        private const char _aiPlayer = 'O';

        [Test]
        public void ConvertTest()
        {
            char[,] b;
            b = new char[,]
                {
                {X,O,X},
                {X,O,O},
                {O,X,X},
                };

            var t1 = new int[,]
            {
                { 1,-1, 1},
                { 1,-1,-1},
                {-1, 1, 1},
            };

            var t2 = new int[,]
            {
                {-1, 1,-1},
                {-1, 1, 1},
                { 1,-1,-1},
            };

            var triBoard = b.Convert2D(ch => Move.ConvertTo(ch, 'O', 'X'));
            Assert.That(triBoard, Is.EqualTo(t2));
            triBoard = b.Convert2D(ch => Move.ConvertTo(ch, 'X', 'O'));
            Assert.That(triBoard, Is.EqualTo(t1));
            var newboard = triBoard.Convert2D(tri => Move.ConvertTo(tri, 'X', 'O', '\0'));
            Assert.That(newboard, Is.EqualTo(b));


        }

        [Test]
        public void IsAiWinTests()
        {
            char[,] b,b2;
            // const MoveState expectedState = MoveState.AiWin;

            b = new char[,]
            {
                {O,O,O},
                {_,O,_},
                {_,_,_},
            };
            b2 = _engine.GetMove(b, 'X', 'O');
            Assert.That(b, Is.EqualTo(b2));

            b = new char[,]
            {
                {O,_,_},
                {O,O,_},
                {O,_,_},
            };
            b2 = _engine.GetMove(b, 'X', 'O');
            Assert.That(b, Is.EqualTo(b2));

            b = new char[,]
            {
                {X,_,O},
                {X,O,_},
                {O,_,_},
            };
            b2 = _engine.GetMove(b, 'X', 'O');
            Assert.That(b, Is.EqualTo(b2));

            b = new char[,]
            {
                {O,_,_},
                {X,O,_},
                {X,_,O },
            };
            b2 = _engine.GetMove(b, 'X', 'O');
            Assert.That(b, Is.EqualTo(b2));
        }

        [Test]
        public void IsCatGameTests()
        {
            char[,] b,b2;
            b = new char[,]
            {
                {X,O,X},
                {X,O,O},
                {O,X,X},
            };
            b2 = _engine.GetMove(b, 'X', 'O');
            Assert.That(b, Is.EqualTo(b2));

        }

        [Test]
        public void TallyTest1()
        {
            
        }
    }
}