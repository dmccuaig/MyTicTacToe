namespace TicTacToe.Engine.AI
{
    public interface INode
    {
        int Player { get; }
        int Opponent { get; }
        INode[] Children { get; }
        double Score { get; }
        int Depth { get; set; }
    }
}