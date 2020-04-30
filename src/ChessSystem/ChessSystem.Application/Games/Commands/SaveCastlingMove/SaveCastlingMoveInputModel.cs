using ChessSystem.Domain.Entities;

namespace ChessSystem.Application.Games.Commands.SaveCastlingMove
{
    public class SaveCastlingMoveInputModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaveCastlingMoveInputModel"/> class.
        /// </summary>
        /// <param name="colorOfTheFigures"></param>
        /// <param name="kingInitialPositionHorizontal"></param>
        /// <param name="kingInitialPositionVertical"></param>
        /// <param name="kingTargetPositionHorizontal"></param>
        /// <param name="kingTargetPositionVertical"></param>
        /// <param name="rookInitialPositionHorizontal"></param>
        /// <param name="rookInitialPositionVertical"></param>
        /// <param name="rookTargetPositionHorizontal"></param>
        /// <param name="rookTargetPositionVertical"></param>
        public SaveCastlingMoveInputModel(
            ChessFigureColor colorOfTheFigures,
            char kingInitialPositionHorizontal,
            int kingInitialPositionVertical,
            char rookInitialPositionHorizontal,
            int rookInitialPositionVertical)
        {
            this.ColorOfTheFigures = colorOfTheFigures;
            this.KingInitialPositionHorizontal = kingInitialPositionHorizontal;
            this.KingInitialPositionVertical = kingInitialPositionVertical;
            this.RookInitialPositionHorizontal = rookInitialPositionHorizontal;
            this.RookInitialPositionVertical = rookInitialPositionVertical;
        }

        public ChessFigureColor ColorOfTheFigures { get; set; }

        public char KingInitialPositionHorizontal { get; set; }

        public int KingInitialPositionVertical { get; set; }

        public char RookInitialPositionHorizontal { get; set; }

        public int RookInitialPositionVertical { get; set; }
    }
}
