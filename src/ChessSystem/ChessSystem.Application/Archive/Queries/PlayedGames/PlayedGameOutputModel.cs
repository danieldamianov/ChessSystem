﻿using ChessGameLogic.Enums;
using ChessSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace ChessSystem.Application.Archive.Queries.PlayedGames
{
    public class PlayedGameOutputModel
    {
        public string Id { get; set; }

        public string WhitePlayerName { get; set; }

        public string BlackPlayerName { get; set; }

        public DateTime StartTime { get; set; }

        public TimeSpan Duration { get; set; }

        public EndGameInfo EndGameInfo { get; set; }

        public List<BaseMoveOutputModel> Moves { get; set; }

        public ChessColors PlayerColorInTheGame { get; set; }
    }
}