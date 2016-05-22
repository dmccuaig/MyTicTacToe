using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TicTacToe.EngineTests
{
    [TestClass]
    public class ScoringTests
    {
        const char _ = '\0';
        private const char X = 'X';
        private const char O = 'O';


        readonly Engine.AiEngine _engine = new Engine.AiEngine();

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
