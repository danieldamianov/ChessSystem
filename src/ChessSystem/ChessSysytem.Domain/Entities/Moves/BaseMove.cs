using ChessSystem.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessSystem.Domain.Entities.Moves
{
    public abstract class BaseMove : BaseEntitiy<string>
    {
        public BaseMove()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int OrderInTheGame { get; set; }

        public string ChessGameId { get; set; }

        public ChessGame ChessGame { get; set; }
    }
}
