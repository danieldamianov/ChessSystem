namespace ChessGameLogic.ChessFigures.Interfaces
{
    /// <summary>
    /// Interface for Rook and King.
    /// </summary>
    internal interface ICastleableFigure
    {
        /// <summary>
        /// Gets a value indicating whether has the figure been moved from the start of the game.
        /// </summary>
        bool HasBeenMovedFromTheStartOfTheGame { get; }

        /// <summary>
        /// Sets HasBeenMovedFromTheStartOfTheGame to true.
        /// </summary>
        void Move();
    }
}
