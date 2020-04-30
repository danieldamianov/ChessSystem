namespace ChessSystem.Application.OnlineUsers.Queries.GetAllOnlineUsers
{
    using System;

    using ChessSystem.Application.Common.Mapping;
    using ChessSystem.Domain.Entities;

    public class OnlineUserOutputModel : IMapFrom<OnlineUser>
    {
        public string UserId { get; set; }

        public string Username { get; set; }

        public DateTime OnlineSince { get; set; }
    }
}
