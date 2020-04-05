namespace ChessGameLogic.ChessFigures.Interfaces
{
    using System.Collections.Generic;

    using ChessGameLogic.ChessMoves;

    internal interface IUnableToJumpFigure
    {
        IEnumerable<ChessBoardPosition> GetPositionsInTheWayOfMove(NormalChessMovePositions normalMove);
    }
}
