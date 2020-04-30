namespace ChessSystem.Domain.Entities
{
    using System;

    using ChessSystem.Domain.BaseEntities;

    public class OnlineUser : BaseEntitiy<string>
    {
        public OnlineUser()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string UserId { get; set; }

        public string Username { get; set; }

        public DateTime OnlineSince { get; set; }
    }
}
