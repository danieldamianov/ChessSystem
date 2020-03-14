using ChessGameLogic.ChessMoves;
using ChessGameLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGameLogic.ChessFigures.Interfaces
{
    internal interface IFigure
    {
        bool AreMovePositionsPossible(NormalChessMovePositions normalMove);

        ChessColors Color { get; }
    }
}
