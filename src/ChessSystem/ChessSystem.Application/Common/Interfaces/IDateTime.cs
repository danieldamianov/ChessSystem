namespace ChessSystem.Application.Common.Interfaces
{
    using System;

    using ChessSystem.Application.Common.ServiceInterfaces;

    public interface IDateTime : IService
    {
        DateTime Now { get; }
    }
}
