using System;

namespace ChessGameLogic.Exceptions
{
    public class PositionOutOfBoardException : Exception
    {
        public PositionOutOfBoardException(string message) 
            : base(message)
        {
        }
    }
}
