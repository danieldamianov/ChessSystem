using ChessSystem.Application.Common.Mapping;
using ChessSystem.Domain.Entities;
using ChessSystem.Domain.Entities.Moves;

namespace ChessSystem.Application.Archive.Queries.PlayedGames
{
    public class NormalMoveOutputModel : BaseMoveOutputModel, IMapFrom<NormalMove>
    {
        public PositionOutputModel InitialPosition { get; set; }

        public PositionOutputModel TargetPosition { get; set; }

        public ChessFigureType ChessFigureType { get; set; }

        public ChessFigureColor ChessFigureColor { get; set; }
    }
}
