namespace ChessSystem.Domain.Entities.Moves
{
    public class NormalMove : BaseMove
    {
        public NormalMove(int orderInTheGame, ChessFigureType chessFigureType, ChessFigureColor chessFigureColor)
            : base(orderInTheGame)
        {
            this.ChessFigureType = chessFigureType;
            this.ChessFigureColor = chessFigureColor;
        }

        public string InitialPositionId { get; set; }

        public ChessBoardPosition InitialPosition { get; set; }

        public string TargetPositionId { get; set; }

        public ChessBoardPosition TargetPosition { get; set; }

        public ChessFigureType ChessFigureType { get; set; }

        public ChessFigureColor ChessFigureColor { get; set; }
    }
}
