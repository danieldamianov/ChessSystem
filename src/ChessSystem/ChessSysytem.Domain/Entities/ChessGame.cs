namespace ChessSystem.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    using ChessSystem.Domain.BaseEntities;

    using ChessSystem.Domain.Entities.Moves;

    /// <summary>
    /// Main model for storing a classic chess game.
    /// </summary>
    public class ChessGame : BaseEntitiy<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChessGame"/> class.
        /// </summary>
        /// <param name="whitePlayerId"></param>
        /// <param name="blackPlayerId"></param>
        public ChessGame(string whitePlayerId, string blackPlayerId)
            : this()
        {
            if (whitePlayerId == null || blackPlayerId == null)
            {
                throw new ArgumentNullException("id", "Player id cannot be null!");
            }

            this.WhitePlayerId = whitePlayerId;
            this.BlackPlayerId = blackPlayerId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChessGame"/> class.
        /// </summary>
        protected ChessGame()
        {
            this.Id = Guid.NewGuid().ToString();

            this.NormalChessMoves = new List<NormalMove>();
            this.PawnProductionMoves = new List<PawnProductionMove>();
            this.CastlingMoves = new List<CastlingMove>();
        }

        public string WhitePlayerId { get; set; }

        public string BlackPlayerId { get; set; }

        public List<NormalMove> NormalChessMoves { get; set; }

        public List<PawnProductionMove> PawnProductionMoves { get; set; }

        public List<CastlingMove> CastlingMoves { get; set; }

        public EndGameInfo? EndGameInfo { get; set; }

        public DateTime StartedOn { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
