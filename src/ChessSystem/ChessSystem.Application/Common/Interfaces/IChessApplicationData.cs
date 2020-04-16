namespace ChessSystem.Application.Common.Interfaces
{
    using System.Threading.Tasks;
    using System.Threading;
    using Microsoft.EntityFrameworkCore;
    using ChessSystem.Domain.Entities;

    public interface IChessApplicationData
    {
        Task<int> SaveChanges(CancellationToken cancellationToken);

        public DbSet<OnlineUser> LogedInUsers { get; set; }

        OnlineUser Single();
    }
}
