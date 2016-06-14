using NUnit.Framework;
using TicTacToe.Engine;

namespace Engine.Test
{
    [TestFixture]
    public class NodeTests
    {
        const int P = Move.Player, O = Move.Opponent, _ = Move.None;

        [Test]
        public void TallyTest1()
        {
            int[,] t;
            TicTacToeNode n;

            t = new int[,]
            {
                { O,P, O},
                { O,P,P},
                {P, O, O},
            };

            n = new TicTacToeNode(Move.Player, t);
            //Assert.That(n._rowCounts[0], Is.EqualTo(O+P+O));
            //Assert.That(n._rowCounts[1], Is.EqualTo(O + P + P));
            //Assert.That(n._rowCounts[2], Is.EqualTo(P+O+O));
            //Assert.That(n._colCounts[0], Is.EqualTo(O+O+P));
            //Assert.That(n._colCounts[1], Is.EqualTo(P+P+O));
            //Assert.That(n._colCounts[2], Is.EqualTo(O+P+O));
            //Assert.That(n._diagCounts, Is.EqualTo(O+P+O));
            //Assert.That(n._backDiagCounts, Is.EqualTo(O+P+P));
            //Assert.That(n.IsTerminal, Is.True);


            var t2 = new int[,]
            {
                {P, O,P},
                {P, O, O},
                { O,P,P},
            };

            var t3 = new int[,]
{
                {O,O,O},
                {_,O,_},
                {_,_,_},
};

            var t4 = new int[,]
   {
                {O,_,_},
                {O,O,_},
                {O,_,_},
   };

            var t5 = new int[,]
            {
                {P,_,O},
                {P,O,_},
                {O,_,_},
            };

            var t6 = new int[,]
            {
                {O,_,_},
                {P,O,_},
                {P,_,O },
            };

            var t7 = new int[,]
            {
                {P, O, P},
                {P, O, O},
                {O, P, P},
            };

        }

    }
}