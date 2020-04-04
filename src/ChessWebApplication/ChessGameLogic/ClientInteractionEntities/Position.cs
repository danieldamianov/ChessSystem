namespace ChessGameLogic.ClientInteractionEntities
{
    using ChessGameLogic.Exceptions;
    using ChessGameLogic.Validations;

    /// <summary>
    /// Class that represents a classic chess board position with standart chess notation.
    /// </summary>
    public class Position
    {
        internal Position(char horizontal, int vertical)
        {
            if (PositionValidator.ValidatePosition(horizontal, vertical) == false)
            {
                throw new PositionOutOfBoardException($"Positions are out of the board! Horiontal : {horizontal} Vertical : {vertical}");
            }

            this.Horizontal = horizontal;
            this.Vertical = vertical;
        }

        /// <summary>
        /// Gets the horizontal - letter from a to h.
        /// </summary>
        public char Horizontal { get; }

        /// <summary>
        /// Gets the vertical - number from 1 to 8.
        /// </summary>
        public int Vertical { get; }
    }
}
