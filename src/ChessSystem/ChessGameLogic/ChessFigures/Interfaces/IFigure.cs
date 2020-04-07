namespace ChessGameLogic.ChessFigures.Interfaces
{
    using ChessGameLogic.ChessMoves;
    using ChessGameLogic.Enums;

    internal interface IFigure
    {
        bool AreMovePositionsPossible(NormalChessMovePositions normalMove);

        ChessColors Color { get; }
    }
}
