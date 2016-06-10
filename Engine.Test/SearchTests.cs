using NUnit.Framework;
using TicTacToe.Engine;
using TicTacToe.Engine.AI;

namespace Engine.Test
{
    public class SearchTests
    {
        readonly AiEngine _engine = new AiEngine();

        const sbyte P = Move.Player, O = Move.Opponent, _ = Move.None;

        sbyte[,] b0 = new sbyte[,]
        {
            {_,_,_},
            {_,_,_},
            {_,_,_},
        };

        sbyte[,] b1 = new sbyte[,]
        {
            {P,_,_},
            {_,_,_},
            {_,_,_},
        };

        sbyte[,] b2 = new sbyte[,]
        {
            {P,_,_},
            {_,O,_},
            {_,_,_},
        };
        sbyte[,] b3 = new sbyte[,]
        {
            {P,_,_},
            {_,O,_},
            {_,_,P},
        };
        sbyte[,] b4 = new sbyte[,]
        {
            {P,O,_},
            {_,O,_},
            {_,_,P},
        };
        sbyte[,] b5 = new sbyte[,]
        {
            {P,O,_},
            {_,O,_},
            {_,P,P},
        };
        sbyte[,] b6 = new sbyte[,]
        {
            {P,O,_},
            {_,O,_},
            {O,P,P},
        };
        sbyte[,] b7 = new sbyte[,]
        {
            {P,O,_},
            {_,O,P},
            {O,P,P},
        };
        sbyte[,] b8 = new sbyte[,]
        {
            {P,O,O},
            {_,O,P},
            {O,P,P},
        };

        sbyte[,] c0 = new sbyte[,]
{
            {O,P,P},
            {_,O,_},
            {_,O,P},
};

        sbyte[,] c1 = new sbyte[,]
{
            {O,P,P},
            {_,O,O},
            {_,O,P},
};

        [Test]
        public void EndGame()
        {
            var ret = _engine.GetBestMove(c0);
            Assert.That(ret, Is.EqualTo(c1));
        }


    }
}