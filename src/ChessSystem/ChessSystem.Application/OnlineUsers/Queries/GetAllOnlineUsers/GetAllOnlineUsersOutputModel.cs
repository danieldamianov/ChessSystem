using ChessSystem.Application.Common.Mapping;
using ChessSystem.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ChessSystem.Application.OnlineUsers.Queries.GetAllOnlineUsers
{
    public class GetAllOnlineUsersOutputModel
    {
        public List<OnlineUserOutputModel> OnlineUsers { get; set; }
    }
}