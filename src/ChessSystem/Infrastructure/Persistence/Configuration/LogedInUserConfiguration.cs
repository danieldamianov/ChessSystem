namespace Infrastructure.Persistence.Configuration
{
    using ChessSystem.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class LogedInUserConfiguration : IEntityTypeConfiguration<OnlineUser>
    {
        public void Configure(EntityTypeBuilder<OnlineUser> builder)
        {
            builder.HasKey(user => user.Id);
        }
    }
}
