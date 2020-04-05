namespace ChessGameLogic.ChessMoves
{
    internal class NormalChessMovePositions
    {
        private ChessBoardPosition initialPosition;

        private ChessBoardPosition targetPosition;

        internal NormalChessMovePositions(char initialPositionHorizontal, int initialPositionVertical
                , char targetPositionHorizontal, int targetPositionVertical)
        {
            this.InitialPosition = new ChessBoardPosition(initialPositionHorizontal, initialPositionVertical);
            this.TargetPosition = new ChessBoardPosition(targetPositionHorizontal, targetPositionVertical);
        }

        internal ChessBoardPosition InitialPosition
        {
            get { return this.initialPosition; }
            private set { this.initialPosition = value; }
        }

        internal ChessBoardPosition TargetPosition
        {
            get { return this.targetPosition; }
            private set { this.targetPosition = value; }
        }
    }
}
