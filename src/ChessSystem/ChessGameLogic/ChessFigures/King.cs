namespace ChessGameLogic.ChessFigures
{
    using System;

    using ChessGameLogic.ChessFigures.Interfaces;
    using ChessGameLogic.ChessMoves;
    using ChessGameLogic.Enums;

    internal class King : Figure, ICastleableFigure
    {
        internal King(ChessColors color)
            : base(color)
        {
            this.HasBeenMovedFromTheStartOfTheGame = false;
        }

        public bool HasBeenMovedFromTheStartOfTheGame { get; private set; }

        public override bool AreMovePositionsPossible(NormalChessMovePositions normalMove)
        {
            int differenceInHorizontal = Math.Abs(normalMove.InitialPosition.Horizontal - normalMove.TargetPosition.Horizontal);
            int differenceInVertical = Math.Abs(normalMove.InitialPosition.Vertical - normalMove.TargetPosition.Vertical);

            if (differenceInHorizontal == 0 && differenceInVertical == 0)
            {
                return false;
            }

            if (differenceInHorizontal <= 1 && differenceInVertical <= 1)
            {
                return true;
            }

            return false;
        }

        public void Move()
        {
            this.HasBeenMovedFromTheStartOfTheGame = true;
        }
    }
}
