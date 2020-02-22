using ChessGameLogic.Exceptions;
using ChessGameLogic.Validations;
using System;

namespace ChessGameLogic
{
    internal class ChessBoardPosition : IEquatable<ChessBoardPosition>
    {
        private char horizontal;
        private int vertical;

        internal ChessBoardPosition(char horizontal, int vertical)
        {
            if (PositionValidator.ValidatePosition(horizontal, vertical) == false)
            {
                throw new PositionOutOfBoardException($"Positions are out of the board! Horiontal : {horizontal} Vertical : {vertical}");
            }

            this.Horizontal = horizontal;
            this.Vertical = vertical;
        }

        internal char Horizontal
        {
            get { return this.horizontal; }
            private set { this.horizontal = value; }
        }

        internal int Vertical
        {
            get { return this.vertical; }
            private set { this.vertical = value; }
        }

        public bool Equals(ChessBoardPosition other)
        {
            if (this.Horizontal == other.Horizontal && this.Vertical == other.Vertical)
            {
                return true;
            }

            return false;
        }
    }
}
