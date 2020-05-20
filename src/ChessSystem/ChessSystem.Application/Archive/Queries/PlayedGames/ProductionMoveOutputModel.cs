using AutoMapper.Mappers;
using ChessSystem.Application.Common.Mapping;
using ChessSystem.Domain.Entities;
using ChessSystem.Domain.Entities.Moves;

namespace ChessSystem.Application.Archive.Queries.PlayedGames
{
    public class ProductionMoveOutputModel : BaseMoveOutputModel, IMapFrom<PawnProductionMove>
    {
        public PositionOutputModel ChessBoardPosition { get; set; }

        public ChessFigureType FigureThatHasBeenProduced { get; set; }

        public ChessFigureColor ColorOFTheFigures { get; set; }
    }
}
