namespace ChessSystem.Domain.Entities.Moves
{
    public class PawnProductionMove : BaseMove
    {
        public PawnProductionMove(int orderInTheGame)
            : base(orderInTheGame)
        {
        }

        public string ChessBoardPositionId { get; set; }

        public ChessBoardPosition ChessBoardPosition { get; set; }

        public ChessFigureType FigureThatHasBeenProduced { get; set; }

        public ChessFigureColor ColorOFTheFigures { get; set; }
    }
}
