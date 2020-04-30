namespace ChessSystem.Domain.Entities.Moves
{
    using System;

    using ChessSystem.Domain.BaseEntities;

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
