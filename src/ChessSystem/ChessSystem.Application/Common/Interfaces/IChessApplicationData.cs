namespace ChessSystem.Application.Common.Interfaces
{
    using System.Threading.Tasks;
    using System.Threading;
    using Microsoft.EntityFrameworkCore;
    using ChessSystem.Domain.Entities;
    using ChessSystem.Domain.Entities.Moves;

    public interface IChessApplicationData
    {
        Task<int> SaveChanges(CancellationToken cancellationToken);

        public DbSet<OnlineUser> LogedInUsers { get; set; }

        public DbSet<ChessBoardPosition> ChessBoardPositions { get; set; }

        public DbSet<NormalMove> NormalMoves { get; set; }

        public DbSet<CastlingMove> CastlingMoves { get; set; }

        public DbSet<PawnProductionMove> PawnProductionMoves { get; set; }

        public DbSet<ChessGame> ChessGames { get; set; }
    }
}
