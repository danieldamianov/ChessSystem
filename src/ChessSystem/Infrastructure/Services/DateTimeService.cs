namespace Infrastructure.Services
{
    using System;

    using ChessSystem.Application.Common.Interfaces;

    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
