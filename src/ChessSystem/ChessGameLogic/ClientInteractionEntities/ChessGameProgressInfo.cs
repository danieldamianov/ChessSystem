namespace ChessGameLogic.ClientInteractionEntities
{
    /// <summary>
    /// Info about the state of the game.
    /// </summary>
    public enum ChessGameProgressInfo
    {
        BlackHaveWon,
        WhiteHaveWon,
        GameHasEndedDraw,
        WhiteAreUnderCheck,
        BlackAreUnderCheck,
        InProgressWithoutCheck
    }
}
