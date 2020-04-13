namespace ChessSystem.Domain.Exceptions
{
    using System;

    public class InvalidEntityException : Exception
    {
        public InvalidEntityException(string message) 
            : base(message)
        {
        }
    }
}
