namespace ChessGameLogic.SandBox
{
    using System.Windows.Forms;
    using ChessGameLogic.ClientInteractionEntities;
    using ChessGameLogic.Enums;
    using ChessGameLogic.SandBox;

    public class ChessField : Button
    {
        public ChessPosition PositionOnTheBoard { get; set; }

        public ChessFigureType? ChessFigure { get; set; }

        public ChessColors? ChessFigureColor { get; set; }
    }
}