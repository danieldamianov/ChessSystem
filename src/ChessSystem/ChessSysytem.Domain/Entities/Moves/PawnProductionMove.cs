namespace ChessSystem.Domain.Entities.Moves
{
    public class PawnProductionMove : BaseMove
    {
        public PawnProductionMove()
            : base()
        { }

        public string ChessBoardPositionId { get; set; }

        public ChessBoardPosition ChessBoardPosition { get; set; }

        public ChessFigureType FigureThatHasBeenProduced { get; set; }

        public ChessFigureColor ColorOFTheFigures { get; set; }
    }
}
