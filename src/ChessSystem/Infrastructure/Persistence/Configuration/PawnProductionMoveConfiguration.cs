namespace Infrastructure.Persistence.Configuration
{
    using ChessSystem.Domain.Entities.Moves;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PawnProductionMoveConfiguration : IEntityTypeConfiguration<PawnProductionMove>
    {
        public void Configure(EntityTypeBuilder<PawnProductionMove> builder)
        {
            builder.HasKey(move => move.Id);

            builder.HasOne(move => move.ChessBoardPosition)
                .WithOne()
                .HasForeignKey<PawnProductionMove>(move => move.ChessBoardPositionId);
        }
    }
}
