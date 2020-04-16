using ChessSystem.Application.Common.Mapping;
using ChessSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessSystem.Application.OnlineUsers.Queries.GetAllOnlineUsers
{
    public class OnlineUserOutputModel : IMapFrom<OnlineUser>
    {
        public string UserId { get; set; }

        public DateTime OnlineSince { get; set; }
    }
}
