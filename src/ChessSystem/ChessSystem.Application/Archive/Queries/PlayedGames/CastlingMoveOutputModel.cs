using ChessSystem.Application.Common.Mapping;
using ChessSystem.Domain.Entities;
using ChessSystem.Domain.Entities.Moves;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessSystem.Application.Archive.Queries.PlayedGames
{
    public class CastlingMoveOutputModel : BaseMoveOutputModel, IMapFrom<CastlingMove>
    {
        public ChessFigureColor ColorOfTheFigures { get; set; }

        public PositionOutputModel KingInitialPosition { get; set; }

        public PositionOutputModel RookInitialPosition { get; set; }
    }
}
