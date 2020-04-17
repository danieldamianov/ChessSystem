using ChessSystem.Application.Common.Mapping;
using ChessSystem.Application.OnlineUsers.Queries.GetAllOnlineUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessWebApplication.Hubs.OnlineUsers
{

    public class OnlineUserSocketModel : IMapFrom<OnlineUserOutputModel>
    {
        public OnlineUserSocketModel(string username, string userId)
        {
            UserId = userId;
            Username = username;
        }

        public string UserId { get; set; }

        public string Username { get; set; }
    }
}

