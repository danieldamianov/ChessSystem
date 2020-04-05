namespace ChessGameLogic.ClientInteractionEntities
{
    public enum CastlingMoveValidationResult
    {
        ValidCastling,
        GameHasEnded,
        PlayerIsNotOnTurn,
        KingWithGivenColorNotFountOntheGivenPoition,
        RookWithGivenColorNotFountOntheGivenPoition,
        FiguresHaveBeenMovedFromTheStartOfTheGame,
        TheKingThatHasToMakeTheCastlingIsUnderCheck,
        ThereIsFigureBetweenTheRookAndTheKing,
        SomeOfTheFieldsBetweenTheRookAndTheKingAreUnderCheck,
    }
}
