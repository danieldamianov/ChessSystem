﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChessSystem.Domain.Entities.Moves
{
    public class CastlingMove : BaseMove
    {
        public CastlingMove(int orderInTheGame)
            : base(orderInTheGame)
        { }

        public ChessFigureColor ColorOfTheFigures { get; set; }

        public string KingInitialPositionId { get; set; }

        public ChessBoardPosition KingInitialPosition { get; set; }

        public string KingTargetPositionnId { get; set; }

        public ChessBoardPosition KingTargetPosition { get; set; }

        public string RookInitialPositionnId { get; set; }

        public ChessBoardPosition RookInitialPosition { get; set; }

        public string RookTargetPositionnId { get; set; }

        public ChessBoardPosition RookTargetPosition { get; set; }
    }
}
