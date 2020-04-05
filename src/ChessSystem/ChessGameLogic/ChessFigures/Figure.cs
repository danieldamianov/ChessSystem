namespace ChessGameLogic.ChessFigures
{
    using ChessGameLogic.ChessFigures.Interfaces;
    using ChessGameLogic.ChessMoves;
    using ChessGameLogic.Enums;

    internal abstract class Figure : IFigure
    {
        protected Figure(ChessColors color)
        {
            this.Color = color;
        }

        public ChessColors Color { get; }

        public abstract bool AreMovePositionsPossible(NormalChessMovePositions normalMove);
    }
}
