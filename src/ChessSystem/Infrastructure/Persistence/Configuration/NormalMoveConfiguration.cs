namespace Infrastructure.Persistence.Configuration
{
    using ChessSystem.Domain.Entities.Moves;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class NormalMoveConfiguration : IEntityTypeConfiguration<NormalMove>
    {
        public void Configure(EntityTypeBuilder<NormalMove> builder)
        {
            builder.HasKey(move => move.Id);

            builder.HasOne(move => move.InitialPosition)
                .WithOne()
                .HasForeignKey<NormalMove>(move => move.InitialPositionId);

            builder.HasOne(move => move.TargetPosition)
                .WithOne()
                .HasForeignKey<NormalMove>(move => move.TargetPositionId);
        }
    }
}
