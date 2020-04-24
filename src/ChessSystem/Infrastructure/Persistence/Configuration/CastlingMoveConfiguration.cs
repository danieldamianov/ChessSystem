using ChessSystem.Domain.Entities.Moves;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configuration
{
    public class CastlingMoveConfiguration : IEntityTypeConfiguration<CastlingMove>
    {
        public void Configure(EntityTypeBuilder<CastlingMove> builder)
        {
            builder.HasKey(move => move.Id);

            builder.HasOne(move => move.KingInitialPosition)
                .WithOne()
                .HasForeignKey<CastlingMove>(move => move.KingInitialPositionId);

            builder.HasOne(move => move.KingTargetPosition)
                .WithOne()
                .HasForeignKey<CastlingMove>(move => move.KingTargetPositionnId);

            builder.HasOne(move => move.RookInitialPosition)
                .WithOne()
                .HasForeignKey<CastlingMove>(move => move.RookInitialPositionnId);

            builder.HasOne(move => move.RookTargetPosition)
                .WithOne()
                .HasForeignKey<CastlingMove>(move => move.RookTargetPositionnId);
        }
    }
}
