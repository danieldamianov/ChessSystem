using System;
using System.Collections.Generic;
using System.Text;

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
            get { return initialPosition; }
            private set { initialPosition = value; }
        }

        internal ChessBoardPosition TargetPosition
        {
            get { return targetPosition; }
            private set { targetPosition = value; }
        }
    }
}
