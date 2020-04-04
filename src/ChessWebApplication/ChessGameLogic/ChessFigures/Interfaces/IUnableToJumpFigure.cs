using ChessGameLogic.ChessMoves;
using System.Collections.Generic;

namespace ChessGameLogic.ChessFigures.Interfaces
{
    internal interface IUnableToJumpFigure
    {
        IEnumerable<ChessBoardPosition> GetPositionsInTheWayOfMove(NormalChessMovePositions normalMove);
    }
}
