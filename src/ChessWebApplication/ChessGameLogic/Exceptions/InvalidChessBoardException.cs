using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGameLogic.Exceptions
{
    public class InvalidChessBoardException : Exception
    {
        public InvalidChessBoardException(string message) : base(message)
        {
        }
    }
}
