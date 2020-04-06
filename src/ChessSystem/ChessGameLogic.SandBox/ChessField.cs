namespace ChessGameLogic.SandBox
{
    using System.Windows.Forms;
    using ChessGameLogic.ClientInteractionEntities;
    using ChessGameLogic.Enums;
    using ChessGameLogic.SandBox;

    public class ChessField : Button
    {
        public ChessPosition positionOnTheBoard;
        public ChessFigureType? chessFigure;
        public ChessColors? chessFigureColor;
    }
}