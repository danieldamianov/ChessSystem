namespace ChessWebApplication.Hubs.OnlineUsers
{
    using ChessSystem.Application.Common.Mapping;
    using ChessSystem.Application.OnlineUsers.Queries.GetAllOnlineUsers;

    public class OnlineUserSocketModel : IMapFrom<OnlineUserOutputModel>
    {
        public OnlineUserSocketModel(string username, string userId)
        {
            this.UserId = userId;
            this.Username = username;
        }

        public string UserId { get; set; }

        public string Username { get; set; }
    }
}