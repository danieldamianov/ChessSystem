using ChessSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configuration
{
    public class ChessBoardPositionConfiguration : IEntityTypeConfiguration<ChessBoardPosition>
    {
        public void Configure(EntityTypeBuilder<ChessBoardPosition> builder)
        {
            builder.HasKey(position => position.Id);
        }
    }
}
