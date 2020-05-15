namespace ChessSystem.Application.Games.Commands.SaveMove
{
    using ChessSystem.Domain.Entities;

    public class SaveNormalMoveInputModel
    {
        public SaveNormalMoveInputModel(
            char initialPositionHorizontal,
            int initialPositionVertical,
            char targetPositionHorizontal,
            int targetPositionVertical,
            ChessFigureType chessFigureType,
            ChessFigureColor chessFigureColor)
        {
            this.InitialPositionHorizontal = initialPositionHorizontal;
            this.InitialPositionVertical = initialPositionVertical;
            this.TargetPositionHorizontal = targetPositionHorizontal;
            this.TargetPositionVertical = targetPositionVertical;
            this.ChessFigureType = chessFigureType;
            this.ChessFigureColor = chessFigureColor;
        }

        public char InitialPositionHorizontal { get; set; }

        public int InitialPositionVertical { get; set; }

        public char TargetPositionHorizontal { get; set; }

        public int TargetPositionVertical { get; set; }

        public ChessFigureType ChessFigureType { get; set; }

        public ChessFigureColor ChessFigureColor { get; set; }
    }
}
