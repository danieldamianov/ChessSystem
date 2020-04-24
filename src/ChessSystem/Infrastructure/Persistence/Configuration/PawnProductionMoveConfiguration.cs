using ChessSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configuration
{
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
