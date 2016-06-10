using NUnit.Framework;
using TicTacToe.Engine;

namespace Engine.Test
{
   // [TestFixture]
    public class BoardTests
    {
        const char _ = '\0';
        private const char X = 'X';
        private const char O = 'O';

        readonly AiEngine _engine = new AiEngine();
        private char[,] retval;

        char[,] b0 = new char[,]
        {
            {_,_,_},
            {_,_,_},
            {_,_,_},
        };

        char[,] b1 = new char[,]
        {
            {X,_,_},
            {_,_,_},
            {_,_,_},
        };

        [Test]
        public void FirstMoveTest()
        {
            retval = _engine.GetMove(b0, 'X', 'O');
            Assert.That(retval, Is.EqualTo(b1));

        }

        char[,] b2 = new char[,]
        {
            {X,_,_},
            {_,O,_},
            {_,_,_},
        };
        char[,] b3 = new char[,]
        {
            {X,_,_},
            {_,O,_},
            {_,_,X},
        };
        char[,] b4 = new char[,]
        {
            {X,O,_},
            {_,O,_},
            {_,_,X},
        };
        char[,] b5 = new char[,]
        {
            {X,O,_},
            {_,O,_},
            {_,X,X},
        };
        char[,] b6 = new char[,]
        {
            {X,O,_},
            {_,O,_},
            {O,X,X},
        };
        char[,] b7 = new char[,]
        {
            {X,O,_},
            {_,O,X},
            {O,X,X},
        };
        char[,] b8 = new char[,]
        {
            {X,O,O},
            {_,O,X},
            {O,X,X},
        };



    }
}