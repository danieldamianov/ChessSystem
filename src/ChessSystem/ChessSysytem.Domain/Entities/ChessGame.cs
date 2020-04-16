namespace ChessSystem.Domain.Entities
{
    using BaseEntities;
    using ChessSystem.Domain.Entities.Moves;
    using System;
    using System.Collections.Generic;

    public class ChessGame : BaseEntitiy<string>
    {
        public ChessGame()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string WhitePlayerId { get; set; }

        public string BlackPlayerId { get; set; }

        public List<NormalMove> NormalChessMoves { get; set; }

        public List<PawnProductionMove> PawnProductionMoves { get; set; }

        public List<CastlingMove> CastlingMoves { get; set; }

        public EndGameInfo EndGameInfo { get; set; }

    }
}
