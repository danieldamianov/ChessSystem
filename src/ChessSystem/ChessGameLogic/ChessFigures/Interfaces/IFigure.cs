using ChessGameLogic.ChessMoves;
using ChessGameLogic.Enums;

namespace ChessGameLogic.ChessFigures.Interfaces
{
    internal interface IFigure
    {
        bool AreMovePositionsPossible(NormalChessMovePositions normalMove);

        ChessColors Color { get; }
    }
}
