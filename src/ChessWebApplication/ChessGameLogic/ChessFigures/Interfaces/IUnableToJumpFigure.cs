using ChessGameLogic.ChessMoves;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGameLogic.ChessFigures.Interfaces
{
    internal interface IUnableToJumpFigure
    {
        IEnumerable<ChessBoardPosition> GetPositionsInTheWayOfMove(NormalChessMovePositions normalMove);
    }
}
