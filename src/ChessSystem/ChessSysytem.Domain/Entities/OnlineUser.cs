using ChessSystem.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessSystem.Domain.Entities
{
    public class OnlineUser : BaseEntitiy<string>
    {
        public OnlineUser()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string UserId { get; set; }

        public DateTime OnlineSince { get; set; }
    }
}
