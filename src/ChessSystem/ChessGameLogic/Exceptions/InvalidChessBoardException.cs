using System;

namespace ChessGameLogic.Exceptions
{
    public class InvalidChessBoardException : Exception
    {
        public InvalidChessBoardException(string message)
            : base(message)
        {
        }
    }
}
