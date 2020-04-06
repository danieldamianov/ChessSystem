using ChessGameLogic.Enums;

namespace ChessGameLogic.ClientInteractionEntities
{
    public class ChessFigureOnPositionInfo
    {
        internal ChessFigureOnPositionInfo(ChessFigureType figureType, ChessColors figureColor)
        {
            this.figureType = figureType;
            this.figureColor = figureColor;
        }

        public ChessFigureType figureType { get; }

        public ChessColors figureColor { get; }
    }
}
