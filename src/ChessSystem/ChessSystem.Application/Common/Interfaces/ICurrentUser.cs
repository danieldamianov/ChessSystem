using System;
using System.Collections.Generic;
using System.Text;

namespace ChessSystem.Application.Common.Interfaces
{
    using ChessSystem.Application.Common.ServiceInterfaces;

    public interface ICurrentUser : IScopedService
    {
        string UserId { get; }
    }
}
