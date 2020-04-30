namespace Infrastructure.Persistence.Configuration
{
    using ChessSystem.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ChessGameConfiguration : IEntityTypeConfiguration<ChessGame>
    {
        public void Configure(EntityTypeBuilder<ChessGame> builder)
        {
            builder.HasKey(game => game.Id);

            builder.HasMany(game => game.CastlingMoves)
                .WithOne(move => move.ChessGame)
                .HasForeignKey(move => move.ChessGameId);

            builder.HasMany(game => game.NormalChessMoves)
                .WithOne(move => move.ChessGame)
                .HasForeignKey(move => move.ChessGameId);

            builder.HasMany(game => game.PawnProductionMoves)
                .WithOne(move => move.ChessGame)
                .HasForeignKey(move => move.ChessGameId);
        }
    }
}
