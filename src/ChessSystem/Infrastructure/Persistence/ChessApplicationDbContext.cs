namespace Infrastructure.Persistence
{
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using ChessSystem.Application.Common.Interfaces;
    using ChessSystem.Domain.BaseEntities;
    using ChessSystem.Domain.Entities;
    using ChessSystem.Domain.Entities.Moves;
    using IdentityServer4.EntityFramework.Options;
    using Infrastructure.Identity;
    using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    public class ChessApplicationDbContext : ApiAuthorizationDbContext<ChessAppUser>, IChessApplicationData
    {
        private readonly ICurrentUser currentUserService;
        private readonly IDateTime dateTime;

        public ChessApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUser currentUserService,
            IDateTime dateTime)
            : base(options, operationalStoreOptions)
        {
            this.currentUserService = currentUserService;
            this.dateTime = dateTime;
        }

        public DbSet<OnlineUser> LogedInUsers { get; set; }

        public DbSet<ChessBoardPosition> ChessBoardPositions { get; set; }

        public DbSet<NormalMove> NormalMoves { get; set; }

        public DbSet<CastlingMove> CastlingMoves { get; set; }

        public DbSet<PawnProductionMove> PawnProductionMoves { get; set; }

        public DbSet<ChessGame> ChessGames { get; set; }

        public Task<int> SaveChanges(CancellationToken cancellationToken = new CancellationToken())
            => this.SaveChangesAsync(cancellationToken);

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in this.ChangeTracker.Entries<IAuditInfo>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy ??= this.currentUserService.UserId;
                        entry.Entity.CreatedOn = this.dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = this.currentUserService.UserId;
                        entry.Entity.ModifiedOn = this.dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
