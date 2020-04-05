namespace ChessGameLogic.ClientInteractionEntities
{
    public enum NormalChessMoveValidationResult
    {
        ValidMove,
        GameHasEnded,
        PlayerIsNotOnTurn,
        ThereIsntSuchFigureAndColorOnTheGivenPosition,
        TheFigureOnTheTargetPositionIsFriendlyOrEnemyKing,
        MovePositionsAreNotValid,
        ThereAreOtherPiecesOnTheWay,
        MovementResultsInCheckOfTheFriendlyKing,
    }
}
