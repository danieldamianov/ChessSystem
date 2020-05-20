using ChessSystem.Application.Common.Mapping;
using ChessSystem.Domain.Entities;

namespace ChessSystem.Application.Archive.Queries.PlayedGames
{
    public class PositionOutputModel : IMapFrom<ChessBoardPosition>
    {
        public char Horizontal { get; set; }

        public int Vertical { get; set; }

        public override string ToString()
        {
            return $"{this.Horizontal}{this.Vertical}";
        }
    }
}
