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
