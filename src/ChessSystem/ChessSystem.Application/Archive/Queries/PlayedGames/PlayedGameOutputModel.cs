using ChessSystem.Domain.Entities;
using System;
using System.Security.Principal;

namespace ChessSystem.Application.Archive.Queries.PlayedGames
{
    public class PlayedGameOutputModel
    {
        public string WhitePlayerName { get; set; }

        public string BlackPlayerName { get; set; }

        public DateTime StartTime { get; set; }

        public TimeSpan Duration { get; set; }

        public EndGameInfo EndGameInfo { get; set; }
    }
}