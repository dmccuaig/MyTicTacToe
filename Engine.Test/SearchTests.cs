using NUnit.Framework;
using TicTacToe.Engine;

namespace Engine.Test
{
    public class SearchTests
    {
        readonly AiEngine _engine = new AiEngine();

        const int P = Move.Player, O = Move.Opponent, _ = Move.None;

        int[,] b0 = new int[,]
        {
            {_,_,_},
            {_,_,_},
            {_,_,_},
        };

        int[,] b1 = new int[,]
        {
            {P,_,_},
            {_,_,_},
            {_,_,_},
        };

        int[,] b2 = new int[,]
        {
            {P,_,_},
            {_,O,_},
            {_,_,_},
        };
        int[,] b3 = new int[,]
        {
            {P,_,_},
            {_,O,_},
            {_,_,P},
        };
        int[,] b4 = new int[,]
        {
            {P,O,_},
            {_,O,_},
            {_,_,P},
        };
        int[,] b5 = new int[,]
        {
            {P,O,_},
            {_,O,_},
            {_,P,P},
        };
        int[,] b6 = new int[,]
        {
            {P,O,_},
            {_,O,_},
            {O,P,P},
        };
        int[,] b7 = new int[,]
        {
            {P,O,_},
            {_,O,P},
            {O,P,P},
        };
        int[,] b8 = new int[,]
        {
            {P,O,O},
            {_,O,P},
            {O,P,P},
        };

        int[,] c0 = new int[,]
{
            {O,P,P},
            {_,O,_},
            {_,O,P},
};

        int[,] c1 = new int[,]
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