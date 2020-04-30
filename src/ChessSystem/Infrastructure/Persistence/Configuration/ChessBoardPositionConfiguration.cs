namespace Infrastructure.Persistence.Configuration
{
    using ChessSystem.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ChessBoardPositionConfiguration : IEntityTypeConfiguration<ChessBoardPosition>
    {
        public void Configure(EntityTypeBuilder<ChessBoardPosition> builder)
        {
            builder.HasKey(position => position.Id);
        }
    }
}
