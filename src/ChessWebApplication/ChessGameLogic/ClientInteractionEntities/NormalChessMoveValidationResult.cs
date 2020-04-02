using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGameLogic.ClientInteractionEntities
{
    public enum NormalChessMoveValidationResult
    {
        ValidMove,
        ThereIsntSuchFigureAndColorOnTheGivenPosition,
        TheFigureOnTheTargetPositionIsFriendlyOrEnemyKing,
        MovePositionsAreNotValid,
        ThereAreOtherPiecesOnTheWay,
        MovementResultsInCheckOfTheFriendlyKing,
    }
}
