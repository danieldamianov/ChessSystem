namespace ChessGameLogic.ClientInteractionEntities
{
    public enum NormalChessMoveValidationResult
    {
        ValidMove,
        GameHasEnded,
        ThereIsntSuchFigureAndColorOnTheGivenPosition,
        TheFigureOnTheTargetPositionIsFriendlyOrEnemyKing,
        MovePositionsAreNotValid,
        ThereAreOtherPiecesOnTheWay,
        MovementResultsInCheckOfTheFriendlyKing,
    }
}
