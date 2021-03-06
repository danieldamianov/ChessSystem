﻿namespace ChessSystem.Domain.Entities
{
    using System;

    using ChessSystem.Domain.BaseEntities;
    using ChessSystem.Domain.Exceptions;

    public class ChessBoardPosition : BaseEntitiy<string>
    {
        private char horizontal;
        private int vertical;

        public ChessBoardPosition(char horizontal, int vertical)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Horizontal = horizontal;
            this.Vertical = vertical;
        }

        public char Horizontal
        {
            get { return this.horizontal; }

            private set
            {
                if (value < 'a' && value > 'h')
                {
                    throw new PositionOutOfBoardException("Invalid posotion!!!");
                }

                this.horizontal = value;
            }
        }

        public int Vertical
        {
            get { return this.vertical; }

            private set
            {
                if (value < 1 && value > 8)
                {
                    throw new PositionOutOfBoardException("Invalid posotion!!!");
                }

                this.vertical = value;
            }
        }
    }
}
