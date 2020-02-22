using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGameLogic.Exceptions
{
    public class PositionOutOfBoardException : Exception
    {
        public PositionOutOfBoardException(string message) : base(message)
        {
        }
    }
}
