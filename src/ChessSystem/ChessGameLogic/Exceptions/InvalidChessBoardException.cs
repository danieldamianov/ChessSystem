namespace ChessGameLogic.Exceptions
{
    using System;

    public class InvalidChessBoardException : Exception
    {
        public InvalidChessBoardException(string message)
            : base(message)
        {
        }
    }
}
