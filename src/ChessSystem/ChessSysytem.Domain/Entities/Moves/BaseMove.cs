using ChessSystem.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessSystem.Domain.Entities.Moves
{
    public abstract class BaseMove : BaseEntitiy<string>
    {
        public BaseMove(int orderInTheGame)
        {
            this.Id = Guid.NewGuid().ToString();

            this.OrderInTheGame = orderInTheGame;
        }

        public int OrderInTheGame { get; set; }

        public string ChessGameId { get; set; }

        public ChessGame ChessGame { get; set; }
    }
}
