using ChessGameLogic.ChessFigures.Interfaces;
using ChessGameLogic.ChessMoves;
using ChessGameLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGameLogic.ChessFigures
{
    internal class Rook : Figure , IUnableToJumpFigure, ICastleableFigure
    {
        internal Rook(ChessColors color) : base(color)
        {
        }

        public bool HasBeenMovedFromTheStartOfTheGame { get; private set; }

        public override bool AreMovePositionsPossible(NormalChessMovePositions normalMove)
        {
            if ((normalMove.InitialPosition.Horizontal == normalMove.TargetPosition.Horizontal
                || normalMove.InitialPosition.Vertical == normalMove.TargetPosition.Vertical) && !normalMove.InitialPosition.Equals(normalMove.TargetPosition)
                )
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

            if (normalMove.InitialPosition.Horizontal == normalMove.TargetPosition.Horizontal)
            {
                for (int i = Math.Min(normalMove.InitialPosition.Vertical, normalMove.TargetPosition.Vertical) + 1;
                    i < Math.Max(normalMove.InitialPosition.Vertical, normalMove.TargetPosition.Vertical); i++)
                {
                    positionsOnTheBoard.Add(new ChessBoardPosition(normalMove.InitialPosition.Horizontal, i));
                }
            }

            if (normalMove.InitialPosition.Vertical == normalMove.TargetPosition.Vertical)
            {
                for (int i = Math.Min(normalMove.InitialPosition.Horizontal, normalMove.TargetPosition.Horizontal) + 1;
                    i < Math.Max(normalMove.InitialPosition.Horizontal, normalMove.TargetPosition.Horizontal); i++)
                {
                    positionsOnTheBoard.Add(new ChessBoardPosition((char)i, normalMove.InitialPosition.Vertical));
                }
            }

            return positionsOnTheBoard;
        }

        public void Move()
        {
            this.HasBeenMovedFromTheStartOfTheGame = true;
        }
    }
}
