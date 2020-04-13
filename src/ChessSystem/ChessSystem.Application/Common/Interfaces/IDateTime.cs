using System;
using System.Collections.Generic;
using System.Text;

namespace ChessSystem.Application.Common.Interfaces
{
    using System;
    using ServiceInterfaces;

    public interface IDateTime : IService
    {
        DateTime Now { get; }
    }
}
