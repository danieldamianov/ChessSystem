using ChessGameLogic.ChessFigures.Interfaces;
using ChessGameLogic.ChessMoves;
using ChessGameLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGameLogic.ChessFigures
{
    internal class Pawn : Figure, IUnableToJumpFigure
    {
        internal Pawn(ChessColors color) : base(color)
        {
        }

        internal bool IsAttackingMovePossible(NormalChessMovePositions move)
        {
            if (this.Color == ChessColors.White && move.InitialPosition.Vertical == 1)
            {
                return false;
            }

            if (this.Color == ChessColors.Black && move.InitialPosition.Vertical == 8)
            {
                return false;
            }

            if (this.Color == ChessColors.White)
            {
                int differenceInHorizontal = Math.Abs(move.InitialPosition.Horizontal - move.TargetPosition.Horizontal);
                int differenceInVertical = move.InitialPosition.Vertical - move.TargetPosition.Vertical;

                if (differenceInHorizontal == 1 && differenceInVertical == -1)
                {
                    return true;
                }
            }
            else
            {
                int differenceInHorizontal = Math.Abs(move.InitialPosition.Horizontal - move.TargetPosition.Horizontal);
                int differenceInVertical = move.InitialPosition.Vertical - move.TargetPosition.Vertical;

                if (differenceInHorizontal == 1 && differenceInVertical == 1)
                {
                    return true;
                }
            }

            return false;
        }
        internal bool isPositionProducable(ChessBoardPosition positionOnTheBoard)
        {
            if (this.Color == ChessColors.White)
            {
                return positionOnTheBoard.Vertical == 8;
            }
            else
            {
                return positionOnTheBoard.Vertical == 1;
            }
        }

        public override bool AreMovePositionsPossible(NormalChessMovePositions normalMove)
        {
            int differenceInHorizontal = Math.Abs(normalMove.InitialPosition.Horizontal - normalMove.TargetPosition.Horizontal);
            int differenceInVertical = Math.Abs(normalMove.InitialPosition.Vertical - normalMove.TargetPosition.Vertical);

            if (this.Color == ChessColors.White && normalMove.InitialPosition.Vertical == 1)
            {
                return false;
            }

            if (this.Color == ChessColors.Black && normalMove.InitialPosition.Vertical == 8)
            {
                return false;
            }

            if (differenceInHorizontal == 0 && (differenceInVertical == 1 || differenceInVertical == 2))
            {
                if (this.Color == ChessColors.White && normalMove.InitialPosition.Vertical == 2 && normalMove.TargetPosition.Vertical == 4)
                {
                    return true;
                }

                if (this.Color == ChessColors.Black && normalMove.InitialPosition.Vertical == 7 && normalMove.TargetPosition.Vertical == 5)
                {
                    return true;
                }

                if (this.Color == ChessColors.White && normalMove.InitialPosition.Vertical + 1 == normalMove.TargetPosition.Vertical)
                {
                    return true;
                }

                if (this.Color == ChessColors.Black && normalMove.InitialPosition.Vertical - 1 == normalMove.TargetPosition.Vertical)
                {
                    return true;
                }

            }

            return false;
        }

        public IEnumerable<ChessBoardPosition> GetPositionsInTheWayOfMove(NormalChessMovePositions normalMove)
        {
            if (this.AreMovePositionsPossible(normalMove) == false && this.IsAttackingMovePossible(normalMove) == false)
            {
                return null;
            }
            if (this.IsAttackingMovePossible(normalMove))
            {
                return new List<ChessBoardPosition>();
            }
            int differenceInHorizontal = Math.Abs(normalMove.InitialPosition.Horizontal - normalMove.TargetPosition.Horizontal);
            int differenceInVertical = Math.Abs(normalMove.InitialPosition.Vertical - normalMove.TargetPosition.Vertical);


            if (this.Color == ChessColors.White && normalMove.InitialPosition.Vertical == 2 && normalMove.TargetPosition.Vertical == 4)
            {
                return new List<ChessBoardPosition>() { new ChessBoardPosition(normalMove.InitialPosition.Horizontal, 3) };
            }

            if (this.Color == ChessColors.Black && normalMove.InitialPosition.Vertical == 7 && normalMove.TargetPosition.Vertical == 5)
            {
                return new List<ChessBoardPosition>() { new ChessBoardPosition(normalMove.InitialPosition.Horizontal, 6) };
            }



            return new List<ChessBoardPosition>();
        }
    }
}
