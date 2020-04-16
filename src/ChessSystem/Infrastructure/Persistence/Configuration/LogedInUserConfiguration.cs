using ChessSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configuration
{
    public class LogedInUserConfiguration : IEntityTypeConfiguration<OnlineUser>
    {
        public void Configure(EntityTypeBuilder<OnlineUser> builder)
        {
            builder.HasKey(user => user.Id);
        }
    }
}
