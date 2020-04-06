using ChessGameLogic.ClientInteractionEntities;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ChessGameLogic.SandBox
{
    /// <summary>
    /// Class that represents a classic chess board position with standart chess notation.
    /// </summary>
    public class ChessPosition : IEquatable<Position>
    {
        public ChessPosition(char horizontal, int vertical)
        {
            this.Horizontal = horizontal;
            this.Vertical = vertical;
        }

        /// <summary>
        /// Gets the horizontal - letter from a to h.
        /// </summary>
        public char Horizontal { get; }

        /// <summary>
        /// Gets the vertical - number from 1 to 8.
        /// </summary>
        public int Vertical { get; }

        public bool Equals(Position other)
        {
            return this.Horizontal == other.Horizontal
                && this.Vertical == other.Vertical;
        }
    }
}
