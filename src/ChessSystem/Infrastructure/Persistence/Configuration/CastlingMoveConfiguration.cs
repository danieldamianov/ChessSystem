namespace Infrastructure.Persistence.Configuration
{
    using ChessSystem.Domain.Entities.Moves;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CastlingMoveConfiguration : IEntityTypeConfiguration<CastlingMove>
    {
        public void Configure(EntityTypeBuilder<CastlingMove> builder)
        {
            builder.HasKey(move => move.Id);

            builder.HasOne(move => move.KingInitialPosition)
                .WithOne()
                .HasForeignKey<CastlingMove>(move => move.KingInitialPositionId);

            builder.HasOne(move => move.RookInitialPosition)
                .WithOne()
                .HasForeignKey<CastlingMove>(move => move.RookInitialPositionnId);
        }
    }
}
