using ChessGameLogic.ChessMoves;
using ChessGameLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGameLogic.ChessFigures
{
    internal class Knight : Figure
    {
        internal Knight(ChessColors color) : base(color)
        {
        }

        public override bool AreMovePositionsPossible(NormalChessMovePositions normalMove)
        {
            int differenceInHorizontal = Math.Abs(normalMove.InitialPosition.Horizontal - normalMove.TargetPosition.Horizontal);
            int differenceInVertical = Math.Abs(normalMove.InitialPosition.Vertical - normalMove.TargetPosition.Vertical);

            if (differenceInHorizontal == 2 && differenceInVertical == 1)
            {
                return true;
            }

            if (differenceInHorizontal == 1 && differenceInVertical == 2)
            {
                return true;
            }

            return false;
        }
    }
}
