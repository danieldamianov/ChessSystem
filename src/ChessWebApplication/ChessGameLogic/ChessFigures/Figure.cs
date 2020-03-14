using ChessGameLogic.ChessFigures.Interfaces;
using ChessGameLogic.ChessMoves;
using ChessGameLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGameLogic.ChessFigures
{
    internal abstract class Figure : IFigure
    {
        protected Figure(ChessColors color)
        {
            this.Color = color;
        }
        public ChessColors Color { get; }

        public abstract bool AreMovePositionsPossible(NormalChessMovePositions normalMove);
    }
}
