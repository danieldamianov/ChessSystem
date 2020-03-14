using ChessGameLogic.ChessFigures.Interfaces;
using ChessGameLogic.ChessMoves;
using ChessGameLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGameLogic.ChessFigures
{
    internal class Bishop : Figure, IUnableToJumpFigure
    {
        internal Bishop(ChessColors color) : base(color)
        {
        }

        public override bool AreMovePositionsPossible(NormalChessMovePositions normalMove)
        {
            int differenceInHorizontal = Math.Abs(normalMove.InitialPosition.Horizontal - normalMove.TargetPosition.Horizontal);
            int differenceInVertical = Math.Abs(normalMove.InitialPosition.Vertical - normalMove.TargetPosition.Vertical);

            if (differenceInHorizontal == differenceInVertical && differenceInVertical != 0)
            {
                return true;
            }

            return false;
        }

        public IEnumerable<ChessBoardPosition> GetPositionsInTheWayOfMove(NormalChessMovePositions normalMove)
        {
            if (this.AreMovePositionsPossible(normalMove) == false)
            {
                return null;
            }

            List<ChessBoardPosition> positionsOnTheBoard = new List<ChessBoardPosition>();

            int differenceInHorizontal = normalMove.InitialPosition.Horizontal - normalMove.TargetPosition.Horizontal;
            int differenceInVertical = normalMove.InitialPosition.Vertical - normalMove.TargetPosition.Vertical;

            for (int i = 1; i < Math.Abs(differenceInHorizontal); i++)
            {
                if (differenceInHorizontal > 0 && differenceInVertical > 0)
                {
                    positionsOnTheBoard.Add(new ChessBoardPosition((char)(normalMove.InitialPosition.Horizontal - i), normalMove.InitialPosition.Vertical - i));
                }
                if (differenceInHorizontal < 0 && differenceInVertical > 0)
                {
                    positionsOnTheBoard.Add(new ChessBoardPosition((char)(normalMove.InitialPosition.Horizontal + i), normalMove.InitialPosition.Vertical - i));
                }
                if (differenceInHorizontal > 0 && differenceInVertical < 0)
                {
                    positionsOnTheBoard.Add(new ChessBoardPosition((char)(normalMove.InitialPosition.Horizontal - i), normalMove.InitialPosition.Vertical + i));
                }
                if (differenceInHorizontal < 0 && differenceInVertical < 0)
                {
                    positionsOnTheBoard.Add(new ChessBoardPosition((char)(normalMove.InitialPosition.Horizontal + i), normalMove.InitialPosition.Vertical + i));
                }
            }

            return positionsOnTheBoard;
        }
    }
}
