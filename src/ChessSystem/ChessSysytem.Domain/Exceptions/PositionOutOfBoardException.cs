﻿namespace ChessSystem.Domain.Exceptions
{
    using System;

    public class PositionOutOfBoardException : Exception
    {
        public PositionOutOfBoardException(string message)
            : base(message)
        {
        }
    }
}
